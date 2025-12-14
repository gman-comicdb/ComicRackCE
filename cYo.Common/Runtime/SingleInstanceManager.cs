using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using System.Threading.Tasks;

namespace cYo.Common.Runtime;

public sealed class SingleInstanceManager : IDisposable
{
    private readonly Mutex mutex;
    private readonly string pipeName;
    private CancellationTokenSource cts;

    public bool IsFirstInstance { get; }

    public SingleInstanceManager(string appId)
    {
        pipeName = appId + ".pipe";
        mutex = new Mutex(true, appId, out bool created);
        IsFirstInstance = created;
    }

    public void StartServer(Action<string[]> onMessage)
    {
        cts = new CancellationTokenSource();
        _ = Task.Run(() => ListenLoop(onMessage, cts.Token));
    }

    private async Task ListenLoop(Action<string[]> onMessage, CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            using var server = new NamedPipeServerStream(
                pipeName,
                PipeDirection.In,
                1,
                PipeTransmissionMode.Message,
                PipeOptions.Asynchronous);

            await server.WaitForConnectionAsync(token);

            using var reader = new StreamReader(server);
            string payload = await reader.ReadToEndAsync();
            onMessage(payload.Split('|'));
        }
    }

    public void SendToRunningInstance(string[] args)
    {
        using var client = new NamedPipeClientStream(".", pipeName, PipeDirection.Out);
        client.Connect(1000);

        using var writer = new StreamWriter(client) { AutoFlush = true };
        writer.Write(string.Join("|", args));
    }

    public void Dispose()
    {
        cts?.Cancel();
        mutex.Dispose();
    }
}
