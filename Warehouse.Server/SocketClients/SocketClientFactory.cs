using System.Net.Sockets;

namespace Warehouse.Server.SocketClients;

public class SocketClientFactory : ISocketClientFactory
{
    public ISocketClient GetService(System.Net.Sockets.Socket socket) => new SocketClient(socket);

    public ISocketClient GetService() =>
        new SocketClient(
            new System.Net.Sockets.Socket(
                AddressFamily.InterNetworkV6,
                SocketType.Stream,
                ProtocolType.Tcp
            )
        );
}
