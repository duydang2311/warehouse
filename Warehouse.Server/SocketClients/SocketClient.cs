using System.Net.Sockets;

namespace Warehouse.Server.SocketClients;

public partial class SocketClient : IDisposable
{
    private readonly Socket socket;
    private bool disposed;

    public SocketClient(Socket socket)
    {
        this.socket = socket;
        InitCommunicateInternal();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            socket.Close();
            socket.Dispose();
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
