using System.Net.Sockets;
using Warehouse.Shared.Packets.Serializers;

namespace Warehouse.Server.SocketClients;

public class SocketClientFactory : ISocketClientFactory
{
    private readonly IPacketDataSerializer serializer;

    public SocketClientFactory(IPacketDataSerializer serializer)
    {
        this.serializer = serializer;
    }

    public ISocketClient GetService(System.Net.Sockets.Socket socket) =>
        new SocketClient(serializer, socket);

    public ISocketClient GetService() =>
        new SocketClient(
            serializer,
            new System.Net.Sockets.Socket(
                AddressFamily.InterNetworkV6,
                SocketType.Stream,
                ProtocolType.Tcp
            )
        );
}
