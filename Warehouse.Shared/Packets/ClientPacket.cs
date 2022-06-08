using MessagePack;

namespace Warehouse.Shared.Packets;

[MessagePackObject(true)]
public class ClientPacket : Packet, IClientPacket
{
    public ClientPacketType Type { get; protected set; }

    public ClientPacket(ClientPacketType type)
    {
        Type = type;
    }
}
