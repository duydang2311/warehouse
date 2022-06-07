using Warehouse.Shared.Sockets;
using Warehouse.Shared.Packets.Serializers;

namespace Warehouse.Client.ClientSockets;

public partial class ClientSocket : Socket, IClientSocket
{
    private readonly IPacketDataSerializer serializer;

    public ClientSocket(IPacketDataSerializer serializer, System.Net.Sockets.Socket socket)
        : base(socket)
    {
        this.serializer = serializer;
    }
}
