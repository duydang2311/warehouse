using System.Net.Sockets;
using Warehouse.Shared.Services;
using Warehouse.Shared.Sockets;

namespace Warehouse.Client.ClientSockets;

public class ClientSocketFactory : IClientSocketFactory
{
    public IClientSocket GetService() =>
        new ClientSocket(
            new System.Net.Sockets.Socket(
                AddressFamily.InterNetworkV6,
                SocketType.Stream,
                ProtocolType.Tcp
            )
        );

    public IClientSocket GetService(System.Net.Sockets.Socket socket) => new ClientSocket(socket);
}
