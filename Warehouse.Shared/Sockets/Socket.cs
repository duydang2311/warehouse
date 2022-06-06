using System.Net.Sockets;

namespace Warehouse.Shared.Sockets;

public partial class Socket : ISocket
{
    private bool disposed;
    private readonly System.Net.Sockets.Socket socket;

    public Socket(System.Net.Sockets.Socket socket)
    {
        this.socket = socket;
    }

    public Socket(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType)
    {
        socket = new System.Net.Sockets.Socket(addressFamily, socketType, protocolType);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            socket.Dispose();
            disposed = true;
        }
    }

    ~Socket()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
