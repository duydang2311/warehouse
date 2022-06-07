using MessagePack;

namespace Warehouse.Shared.Packets;

[MessagePackObject(true)]
public class ClientPacketData : PacketData, IClientPacketData
{
    public ClientPacketType Type { get; protected set; }

    public ClientPacketData(ClientPacketType type)
    {
        Type = type;
    }
}
