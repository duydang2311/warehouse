using System.Net;
using System.Net.Sockets;

namespace Warehouse.Shared.Sockets;

public class SocketFactory : ISocketFactory
{
    public ISocket GetService(System.Net.Sockets.Socket socket)
    {
        return new Socket(socket);
    }

    public ISocket GetService()
    {
        return new Socket(
            new System.Net.Sockets.Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp
            )
        );
    }
}
