using Warehouse.Shared.Sockets;
using Warehouse.Shared.Packets.Serializers;

namespace Warehouse.Server.SocketClients;

public partial class SocketClient : Socket, ISocketClient
{
    private readonly IPacketDataSerializer serializer;

    public SocketClient(IPacketDataSerializer serializer, System.Net.Sockets.Socket socket)
        : base(socket)
    {
        this.serializer = serializer;
    }
}
