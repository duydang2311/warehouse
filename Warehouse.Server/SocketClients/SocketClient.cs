using System.Net.Sockets;

namespace Warehouse.Server.SocketClients;

public partial class SocketClient : ISocketClient
{
    private readonly Socket client;
    private bool disposed;

    public SocketClient(Socket client)
    {
        this.client = client;
        InitCommunicateInternal();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            client.Shutdown(SocketShutdown.Both);
            client.Close();
            client.Dispose();
            disposed = true;
        }
    }

    ~SocketClient()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
