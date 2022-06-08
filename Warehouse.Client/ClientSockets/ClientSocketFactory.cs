using System.Net.Sockets;
using Warehouse.Shared.Packets.Serializers;

namespace Warehouse.Client.ClientSockets;

public class ClientSocketFactory : IClientSocketFactory
{
    private readonly IPacketSerializer serializer;

    public ClientSocketFactory(IPacketSerializer serializer)
    {
        this.serializer = serializer;
    }

    public IClientSocket GetService() =>
        new ClientSocket(
            serializer,
            new System.Net.Sockets.Socket(
                AddressFamily.InterNetworkV6,
                SocketType.Stream,
                ProtocolType.Tcp
            )
        );

    public IClientSocket GetService(System.Net.Sockets.Socket socket) =>
        new ClientSocket(serializer, socket);
}
