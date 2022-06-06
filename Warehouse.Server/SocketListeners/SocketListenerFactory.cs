using System.Net;
using System.Net.Sockets;

namespace Warehouse.Server.SocketListeners;

public class SocketListenerFactory : ISocketListenerFactory
{
    public ISocketListener GetService(System.Net.Sockets.Socket socket) =>
        new SocketListener(socket);

    public ISocketListener GetService() =>
        new SocketListener(
            new System.Net.Sockets.Socket(
                AddressFamily.InterNetworkV6,
                SocketType.Stream,
                ProtocolType.Tcp
            )
        );
}
