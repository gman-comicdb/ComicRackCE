using System;
using System.ComponentModel;
using System.Threading;
using cYo.Common.ComponentModel;
using cYo.Common.Net;
using cYo.Common.Threading;
using cYo.Projects.ComicRack.Engine.Database;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;

namespace cYo.Projects.ComicRack.Engine.IO.Network
{
	public class ComicLibraryClient : DisposableObject
	{
		private IRemoteComicLibrary remoteLibrary;

		private ProcessingQueue<string> queue;

		public IRemoteComicLibrary RemoteLibrary
		{
			get
			{
#if NET10_0_OR_GREATER
                IContextChannel serviceChannel = remoteLibrary as IContextChannel;
#else
				IServiceChannel serviceChannel = remoteLibrary as IServiceChannel;
#endif
                if (serviceChannel.State == CommunicationState.Faulted || serviceChannel.State == CommunicationState.Closed)
				{
					Connect();
				}
				return remoteLibrary;
			}
			set
			{
				remoteLibrary = value;
			}
		}

		public string Password
		{
			get;
			set;
		}

		public ShareInformation ShareInformation
		{
			get;
			private set;
		}

		private ComicLibraryClient(string serviceAddress, ShareInformation information)
		{
			serviceAddress = ServiceAddress.CompletePortAndPath(serviceAddress, ComicLibraryServerConfig.DefaultPrivateServicePort.ToString(), ComicLibraryServerConfig.DefaultServiceName);
			if (information == null)
			{
				IRemoteServerInfo serverInfoService = GetServerInfoService(serviceAddress);
				information = new ShareInformation
				{
					Id = serverInfoService.Id,
					Name = serverInfoService.Name,
					Comment = serverInfoService.Description,
					Options = serverInfoService.Options
				};
			}
			information.Uri = serviceAddress;
			ShareInformation = information;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				queue.SafeDispose();
			}
			base.Dispose(disposing);
		}

		public bool Connect()
		{
			try
			{
				remoteLibrary = GetComicLibraryService(ShareInformation.Uri, Password);
				return remoteLibrary.IsValid;
			}
			catch (MessageSecurityException)
			{
				RemoteLibrary = null;
				return false;
			}
			catch
			{
				RemoteLibrary = null;
				throw;
			}
		}

		public ComicLibrary GetRemoteLibrary()
		{
			try
			{
				byte[] libraryData = RemoteLibrary.GetLibraryData();
				ComicLibrary comicLibrary = ComicLibrary.FromByteArray(libraryData);
				comicLibrary.EditMode = (ShareInformation.IsEditable ? ComicsEditModes.EditProperties : ComicsEditModes.None);
				foreach (ComicBook book in comicLibrary.Books)
				{
					book.FileInfoRetrieved = true;
					book.ComicInfoIsDirty = false;
					book.SetFileLocation($"REMOTE:{comicLibrary.Id}\\{book.FilePath}");
					book.CreateComicProvider += CreateComicProvider;
					if (ShareInformation.IsEditable)
					{
						book.BookChanged += ComicPropertyChanged;
					}
				}
				if (ShareInformation.IsExportable)
				{
					comicLibrary.EditMode |= ComicsEditModes.ExportComic;
				}
				comicLibrary.IsLoaded = true;
				comicLibrary.IsDirty = false;
				return comicLibrary;
			}
			catch (Exception)
			{
				return null;
			}
		}

		private void UpdateComic(ComicBook cb, string propertyName, object value)
		{
			Guid id = cb.Id;
			string item = $"{id}:{propertyName}";
			if (queue == null)
			{
				queue = new ProcessingQueue<string>("Server Book Info Update", ThreadPriority.Highest);
			}
			queue.AddItem(item, delegate
			{
				try
				{
					RemoteLibrary.UpdateComic(id, propertyName, value);
				}
				catch
				{
				}
			});
		}

		private void CreateComicProvider(object sender, CreateComicProviderEventArgs e)
		{
			ComicBook comicBook = sender as ComicBook;
			e.Provider = new RemoteComicBookProvider(comicBook.Id, this);
		}

		private void ComicPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (!(e.PropertyName == "Pages"))
			{
				ComicBook comicBook = sender as ComicBook;
				try
				{
					UpdateComic(comicBook, e.PropertyName, comicBook.GetUntypedPropertyValue(e.PropertyName));
				}
				catch (Exception)
				{
				}
			}
		}

		private static IRemoteComicLibrary GetComicLibraryService(string address, string password)
		{
			string uriString = string.Format("net.tcp://{0}/{1}", address, ComicLibraryServer.LibraryPoint);
#if NET10_0_OR_GREATER
            EndpointAddress remoteAddress = new EndpointAddress(new Uri(uriString));
            ChannelFactory<IRemoteComicLibrary> channelFactory = new ChannelFactory<IRemoteComicLibrary>(CreateChannel(secure: true), remoteAddress);
            channelFactory.Credentials.ServiceCertificate.Authentication.RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck;
#else
			EndpointAddress remoteAddress = new EndpointAddress(new Uri(uriString), EndpointIdentity.CreateDnsIdentity("ComicRack"), (AddressHeaderCollection)null);
			ChannelFactory<IRemoteComicLibrary> channelFactory = new ChannelFactory<IRemoteComicLibrary>(ComicLibraryServer.CreateChannel(secure: true), remoteAddress);
#endif
            channelFactory.Credentials.UserName.UserName = "ComicRack";
			channelFactory.Credentials.UserName.Password = password;
			channelFactory.Credentials.ClientCertificate.Certificate = ComicLibraryServer.Certificate;// New Cert (sha256)
            channelFactory.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;
			IRemoteComicLibrary remoteComicLibrary = channelFactory.CreateChannel();
			((IContextChannel)remoteComicLibrary).OperationTimeout = TimeSpan.FromSeconds(EngineConfiguration.Default.OperationTimeout);
			return remoteComicLibrary;
		}

		private static IRemoteServerInfo GetServerInfoService(string serviceAddress)
		{
			string remoteAddress = string.Format("net.tcp://{0}/{1}", serviceAddress, ComicLibraryServer.InfoPoint);
#if NET10_0_OR_GREATER
            ChannelFactory<IRemoteServerInfo> channelFactory = new ChannelFactory<IRemoteServerInfo>(CreateChannel(secure: false), new EndpointAddress(remoteAddress));
#else
			ChannelFactory<IRemoteServerInfo> channelFactory = new ChannelFactory<IRemoteServerInfo>(ComicLibraryServer.CreateChannel(secure: false), remoteAddress);
#endif
            IRemoteServerInfo remoteServerInfo = channelFactory.CreateChannel();
			((IContextChannel)remoteServerInfo).OperationTimeout = TimeSpan.FromSeconds(EngineConfiguration.Default.OperationTimeout);
			return remoteServerInfo;
		}

#if NET10_0_OR_GREATER
        public static Binding CreateChannel(bool secure)
        {
            NetTcpBinding netTcpBinding = new NetTcpBinding();
            netTcpBinding.Security.Mode = SecurityMode.None;
            //netTcpBinding.Security.Mode = (secure ? SecurityMode.Message : SecurityMode.None);
            //if (secure)
            //{
            //    netTcpBinding.Security.Mode = SecurityMode.TransportWithMessageCredential;
            //    netTcpBinding.Security.Message.ClientCredentialType = MessageCredentialType.UserName;
            //}
            //else
            //{
            //    // Unsecured transport
            //    netTcpBinding.Security.Mode = SecurityMode.None;
            //}
            //netTcpBinding.MaxReceivedMessageSize = 100000000L;
            //netTcpBinding.ReaderQuotas.MaxArrayLength = 100000000;
            return netTcpBinding;
        }
#endif

        public static ComicLibraryClient Connect(string address, ShareInformation information)
		{
			try
			{
				return new ComicLibraryClient(address, information);
			}
			catch (Exception)
			{
				return null;
			}
		}

		public static ComicLibraryClient Connect(string address)
		{
			return Connect(address, null);
		}

		public static ComicLibraryClient Connect(ShareInformation info)
		{
			return Connect(info.Uri, info);
		}

		public static ShareInformation GetServerInfo(string address)
		{
			try
			{
				using (ComicLibraryClient comicLibraryClient = Connect(address))
				{
					return comicLibraryClient.ShareInformation;
				}
			}
			catch (Exception)
			{
				return null;
			}
		}

		public static string GetServerId(string address)
		{
			return GetServerInfo(address)?.Id;
		}
	}
}
