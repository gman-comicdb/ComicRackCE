using System;
using System.Collections.Generic;
using System.Linq;
using cYo.Common.Collections;
using cYo.Common.ComponentModel;
using cYo.Common.Net;
using cYo.Common.Threading;
using cYo.Projects.ComicRack.Engine.IO.Network;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using CoreWCF.Configuration;
using Microsoft.AspNetCore.Hosting;
using CoreWCF.Security;

namespace cYo.Projects.ComicRack.Engine
{
	public class NetworkManager : DisposableObject
	{

        private IHost host;

        private readonly List<IHost> runningHosts = new();

        public class RemoteServerStartedEventArgs : EventArgs
		{
			public ShareInformation Information
			{
				get;
				private set;
			}

			public RemoteServerStartedEventArgs(ShareInformation information)
			{
				Information = information;
			}
		}

		public class RemoteServerStoppedEventArgs : EventArgs
		{
			public string Address
			{
				get;
				private set;
			}

			public RemoteServerStoppedEventArgs(string address)
			{
				Address = address;
			}
		}

		public const int BroadcastPort = 7613;

		private Broadcaster<BroadcastData> broadcaster;

		private readonly SmartList<ComicLibraryServer> runningServers = new SmartList<ComicLibraryServer>();

		private readonly Dictionary<string, ShareInformation> localShares = new Dictionary<string, ShareInformation>();

		public DatabaseManager DatabaseManager
		{
			get;
			private set;
		}

		public CacheManager CacheManager
		{
			get;
			private set;
		}

		public int PrivatePort
		{
			get;
			set;
		}

		public int PublicPort
		{
			get;
			set;
		}

		public bool DisableBroadcast
		{
			get;
			set;
		}

		public ISharesSettings Settings
		{
			get;
			private set;
		}

		public Broadcaster<BroadcastData> Broadcaster
		{
			get
			{
				if (broadcaster == null && !DisableBroadcast)
				{
					broadcaster = new Broadcaster<BroadcastData>(BroadcastPort);
				}
				return broadcaster;
			}
		}

		public SmartList<ComicLibraryServer> RunningServers => runningServers;

		public Dictionary<string, ShareInformation> LocalShares => localShares;

		public event EventHandler<RemoteServerStartedEventArgs> RemoteServerStarted;

		public event EventHandler<RemoteServerStoppedEventArgs> RemoteServerStopped;

		public NetworkManager(DatabaseManager databaseManager, CacheManager cacheManager, ISharesSettings settings, int privatePort, int publicPort, bool disableBroadcast)
		{
			DatabaseManager = databaseManager;
			CacheManager = cacheManager;
			Settings = settings;
			PrivatePort = privatePort;
			PublicPort = publicPort;
			DisableBroadcast = disableBroadcast;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				Stop();
				Broadcaster.SafeDispose();
			}
			base.Dispose(disposing);
		}

		public bool IsOwnServer(string serverAddress)
		{
			return runningServers.Any((ComicLibraryServer rs) => rs.GetAnnouncementUri() == serverAddress);
		}

		public bool HasActiveServers()
		{
			return runningServers.Count > 0;
		}

		public bool RecentServerActivity(int seconds = 10)
		{
			return runningServers.Any((ComicLibraryServer s) => s.Statistics.WasActive(seconds));
		}

		public void BroadcastStart()
		{
			if (Broadcaster != null)
			{
				Broadcaster.Broadcast(new BroadcastData(BroadcastType.ClientStarted));
			}
		}

		public void BroadcastStop()
		{
			if (Broadcaster != null)
			{
				Broadcaster.Broadcast(new BroadcastData(BroadcastType.ClientStopped));
			}
		}

        public void Start()
        {
            foreach (var share in Settings.Shares.Where(sc => sc.IsValidShare))
            {
                var server = new ComicLibraryServer(
                    share,
                    () => DatabaseManager.Database,
                    CacheManager.ImagePool,
                    CacheManager.ImagePool,
                    Broadcaster
                );

                var host = Host.CreateDefaultBuilder()
                    .ConfigureServices(services =>
                    {
                        services.AddSingleton(server);
                        services.AddServiceModelServices()
                                .AddServiceModelMetadata();
                    })
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseNetTcp(options =>
                        {
                            options.Listen($"net.tcp://0.0.0.0:{PrivatePort}");
                            //options.Listen($"net.tcp://0.0.0.0:{share.IsInternet ? PublicPort : PrivatePort}");
                        });
                        webBuilder.Configure(app =>
                        {
                            app.UseServiceModel(sb =>
                            {
                                sb.AddService<ComicLibraryServer>(opts => {
                                    opts.DebugBehavior.IncludeExceptionDetailInFaults = true;
                                });
                                sb.ConfigureServiceHostBase<ComicLibraryServer>(hostBase =>
                                {
                                    hostBase.Credentials.UserNameAuthentication.UserNamePasswordValidationMode =
                                        UserNamePasswordValidationMode.Custom;
									//hostBase.Credentials.UserNameAuthentication.CustomUserNamePasswordValidator =
									//	new CoreWCF.Security.PasswordValidator(share.ProtectionPassword);
									//hostBase.Credentials.ServiceCertificate.Certificate = ComicLibraryServer.Certificate;
                                });

                                sb.AddServiceEndpoint<ComicLibraryServer, IRemoteServerInfo>(
                                    ComicLibraryServer.CreateChannel(secure: false),
                                    $"{share.ServiceName}/{ComicLibraryServer.InfoPoint}");

                                sb.AddServiceEndpoint<ComicLibraryServer, IRemoteComicLibrary>(
                                    ComicLibraryServer.CreateChannel(secure: share.IsInternet),
                                    $"{share.ServiceName}/{ComicLibraryServer.LibraryPoint}");
                            });
                        });
                    })
                    .Build();

                host.Start();
                runningHosts.Add(host);
            }
        }

        public void Stop()
        {
            host?.StopAsync().Wait();
            host?.Dispose();
        }

        private void BroadcasterRecieved(object sender, BroadcastEventArgs<BroadcastData> e)
		{
			if (Broadcaster == null)
			{
				return;
			}
			switch (e.Data.BroadcastType)
			{
			case BroadcastType.ClientStarted:
				foreach (ComicLibraryServer item in runningServers.Where((ComicLibraryServer si) => !si.Config.IsInternet))
				{
					Broadcaster.Broadcast(new BroadcastData(BroadcastType.ServerStarted, item.Config.ServiceName, item.Config.ServicePort));
				}
				break;
			case BroadcastType.ServerStarted:
			{
				string text2 = ServiceAddress.Append(e.Address, e.Data.ServerPort.ToString(), e.Data.ServerName);
				using (ItemMonitor.Lock(localShares))
				{
					if (localShares.ContainsKey(text2))
					{
						return;
					}
				}
				ShareInformation serverInfo = ComicLibraryClient.GetServerInfo(text2);
				if (serverInfo != null)
				{
					using (ItemMonitor.Lock(localShares))
					{
						serverInfo.IsLocal = true;
						localShares[text2] = serverInfo;
					}
					if (Settings.LookForShared && this.RemoteServerStarted != null)
					{
						this.RemoteServerStarted(this, new RemoteServerStartedEventArgs(serverInfo));
					}
				}
				break;
			}
			case BroadcastType.ServerStopped:
			{
				string text = ServiceAddress.Append(e.Address, e.Data.ServerPort.ToString(), e.Data.ServerName);
				using (ItemMonitor.Lock(localShares))
				{
					localShares.Remove(text);
				}
				if (this.RemoteServerStopped != null)
				{
					this.RemoteServerStopped(this, new RemoteServerStoppedEventArgs(text));
				}
				break;
			}
			case BroadcastType.ClientStopped:
				break;
			}
		}
	}
}
