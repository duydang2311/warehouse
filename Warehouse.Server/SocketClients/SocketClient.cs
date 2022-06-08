using Warehouse.Shared.Sockets;
using Warehouse.Shared.Packets.Serializers;

namespace Warehouse.Server.SocketClients;

public partial class SocketClient : Socket, ISocketClient
{
    private readonly IPacketSerializer serializer;

    public SocketClient(IPacketSerializer serializer, System.Net.Sockets.Socket socket)
        : base(socket)
    {
        this.serializer = serializer;
    }
}
