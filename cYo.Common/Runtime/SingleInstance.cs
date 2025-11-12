#define TRACE
using System;
using System.Diagnostics;
using System.ServiceModel;
#if NET10_0_OR_GREATER
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
#endif

namespace cYo.Common.Runtime
{
#if !NET10_0_OR_GREATER
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = true)]
#endif
	public class SingleInstance : ISingleInstance
	{
		private readonly string name;

		private readonly Action<string[]> StartNew;

		private readonly Action<string[]> StartLast;

#if NET10_0_OR_GREATER
        private readonly string pipeName;

        private Mutex mutex;

        private NamedPipeServerStream pipeServer;
#endif

		public SingleInstance(string name, Action<string[]> startNew, Action<string[]> startLast)
		{
			this.name = name;
			StartNew = startNew;
			StartLast = startLast;
#if NET10_0_OR_GREATER
            pipeName = $"Global\\{name}_SingleInstancePipe";
#endif
		}

		public void Run(string[] args)
		{
#if NET10_0_OR_GREATER
            bool isFirstInstance;
            
            mutex = new Mutex(true, $"Global\\{name}_Mutex", out isFirstInstance);
            if (isFirstInstance)
            {
                StartPipeServer();
                StartNew.Invoke(args);
            }
            else
            {
                try
                {
                    // Sends arguments to the existing instance via named pipe.
                    using (var client = new NamedPipeClientStream(".", pipeName, PipeDirection.Out))
                    {
                        client.Connect(1000); // wait up to 1 second
                        using (var writer = new StreamWriter(client, Encoding.UTF8, leaveOpen: false))
                        {
                            writer.WriteLine(string.Join("\0", args ?? Array.Empty<string>()));
                            writer.Flush();
                        }
                    }
                }
                catch
                {
                    // Ignore errors if the existing instance is not responding
                }
            }

			//return isFirstInstance;
#else
			string arg = name;
			string text = $"net.pipe://localhost/{arg}";
			ServiceHost serviceHost = null;
			try
			{
				serviceHost = new ServiceHost(this, new Uri(text));
				serviceHost.AddServiceEndpoint(typeof(ISingleInstance), new NetNamedPipeBinding(), "SI");
				serviceHost.Open();
				try
				{
					StartNew(args);
				}
				catch (Exception ex)
				{
					Trace.WriteLine("Failed to start Program: " + ex.Message);
				}
				return;
			}
			catch (Exception)
			{
			}
			finally
			{
				try
				{
					serviceHost.Close();
				}
				catch
				{
				}
			}
			try
			{
				ChannelFactory<ISingleInstance> channelFactory = new ChannelFactory<ISingleInstance>(new NetNamedPipeBinding(), text + "/SI");
				ISingleInstance singleInstance = channelFactory.CreateChannel();
				singleInstance.InvokeLast(args);
			}
			catch
			{
			}
#endif
		}

		public void InvokeLast(string[] args)
		{
			if (StartLast != null)
			{
				StartLast(args);
			}
		}

		public void InvokeNew(string[] args)
		{
			if (StartNew != null)
			{
				StartNew(args);
			}
		}

#if NET10_0_OR_GREATER
        /// <summary>
        /// Starts the named pipe server to receive arguments from future instances.
        /// </summary>
        private void StartPipeServer()
        {
            pipeServer = new NamedPipeServerStream(pipeName, PipeDirection.In, 1,
                                                    PipeTransmissionMode.Message, PipeOptions.Asynchronous);

            Task.Run(async () =>
            {
                while (pipeServer != null)
                {
                    try
                    {
                        await pipeServer.WaitForConnectionAsync();

                        using (var reader = new StreamReader(pipeServer, Encoding.UTF8, false, 1024, leaveOpen: true))
                        {
                            string line = await reader.ReadLineAsync();
                            if (!string.IsNullOrEmpty(line))
                            {
                                string[] args = line.Split('\0', StringSplitOptions.RemoveEmptyEntries);
                                StartLast?.Invoke(args);
                            }
                        }

                        pipeServer.Disconnect();
                    }
                    catch
                    {
                        // ignore exceptions, continue listening
                    }
                }
            });
        }
        public void Dispose()
        {
            try
            {
                pipeServer?.Dispose();
            }
            catch { }

            try
            {
                mutex?.ReleaseMutex();
                mutex?.Dispose();
            }
            catch { }
        }
#endif
    }
}
