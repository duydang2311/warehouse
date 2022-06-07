using MessagePack;

namespace Warehouse.Shared.Packets;

[MessagePackObject(keyAsPropertyName: true)]
public class Packet : IPacket
{
    public ulong Identity { get; init; }

    [IgnoreMember]
    public byte[] Buffer
    {
        get => MessagePackSerializer.Serialize<IPacket>(this);
    }

    public Packet(ulong identity)
    {
        Identity = identity;
    }
}
