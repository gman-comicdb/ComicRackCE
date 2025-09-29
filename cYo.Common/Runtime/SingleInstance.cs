#define TRACE
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace cYo.Common.Runtime
{

    public class SingleInstance : IDisposable
    {
        private readonly string _appGuid;
        private readonly string _pipeName;
        private Mutex _mutex;
        private NamedPipeServerStream _pipeServer;
        private Action<string[]> _onOtherInstanceStarted;

        public SingleInstance(string appGuid)
        {
            _appGuid = appGuid ?? throw new ArgumentNullException(nameof(appGuid));
            _pipeName = $"Global\\{_appGuid}_SingleInstancePipe";
        }

        /// <summary>
        /// Starts the single-instance check.
        /// Returns true if this is the first instance, false if another instance exists.
        /// </summary>
        public bool Run(string[] args, Action<string[]> onOtherInstanceStarted)
        {
            _onOtherInstanceStarted = onOtherInstanceStarted ?? throw new ArgumentNullException(nameof(onOtherInstanceStarted));

            bool isFirstInstance;
            _mutex = new Mutex(true, $"Global\\{_appGuid}_Mutex", out isFirstInstance);

            if (isFirstInstance)
            {
                StartPipeServer();
            }
            else
            {
                SendArgsToExistingInstance(args);
            }

            return isFirstInstance;
        }

        /// <summary>
        /// Sends arguments to the existing instance via named pipe.
        /// </summary>
        private void SendArgsToExistingInstance(string[] args)
        {
            try
            {
                using (var client = new NamedPipeClientStream(".", _pipeName, PipeDirection.Out))
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

        /// <summary>
        /// Starts the named pipe server to receive arguments from future instances.
        /// </summary>
        private void StartPipeServer()
        {
            _pipeServer = new NamedPipeServerStream(_pipeName, PipeDirection.In, 1,
                                                    PipeTransmissionMode.Message, PipeOptions.Asynchronous);

            Task.Run(async () =>
            {
                while (_pipeServer != null)
                {
                    try
                    {
                        await _pipeServer.WaitForConnectionAsync();

                        using (var reader = new StreamReader(_pipeServer, Encoding.UTF8, false, 1024, leaveOpen: true))
                        {
                            string line = await reader.ReadLineAsync();
                            if (!string.IsNullOrEmpty(line))
                            {
                                string[] args = line.Split('\0', StringSplitOptions.RemoveEmptyEntries);
                                _onOtherInstanceStarted?.Invoke(args);
                            }
                        }

                        _pipeServer.Disconnect();
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
                _pipeServer?.Dispose();
            }
            catch { }

            try
            {
                _mutex?.ReleaseMutex();
                _mutex?.Dispose();
            }
            catch { }
        }
    }

    //[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = true)]
    //public class SingleInstance : ISingleInstance
    //{
    //	private readonly string name;

    //	private readonly Action<string[]> StartNew;

    //	private readonly Action<string[]> StartLast;

    //	public SingleInstance(string name, Action<string[]> startNew, Action<string[]> startLast)
    //	{
    //		this.name = name;
    //		StartNew = startNew;
    //		StartLast = startLast;
    //	}

    //	public void Run(string[] args)
    //	{
    //		string arg = name;
    //		string text = $"net.pipe://localhost/{arg}";
    //		ServiceHost serviceHost = null;
    //		try
    //		{
    //			serviceHost = new ServiceHost(this, new Uri(text));
    //			serviceHost.AddServiceEndpoint(typeof(ISingleInstance), new NetNamedPipeBinding(), "SI");
    //			serviceHost.Open();
    //			try
    //			{
    //				StartNew(args);
    //			}
    //			catch (Exception ex)
    //			{
    //				Trace.WriteLine("Failed to start Program: " + ex.Message);
    //			}
    //			return;
    //		}
    //		catch (Exception)
    //		{
    //		}
    //		finally
    //		{
    //			try
    //			{
    //				serviceHost.Close();
    //			}
    //			catch
    //			{
    //			}
    //		}
    //		try
    //		{
    //			ChannelFactory<ISingleInstance> channelFactory = new ChannelFactory<ISingleInstance>(new NetNamedPipeBinding(), text + "/SI");
    //			ISingleInstance singleInstance = channelFactory.CreateChannel();
    //			singleInstance.InvokeLast(args);
    //		}
    //		catch
    //		{
    //		}
    //	}

    //	public void InvokeLast(string[] args)
    //	{
    //		if (StartLast != null)
    //		{
    //			StartLast(args);
    //		}
    //	}

    //	public void InvokeNew(string[] args)
    //	{
    //		if (StartNew != null)
    //		{
    //			StartNew(args);
    //		}
    //	}
    //}
}
