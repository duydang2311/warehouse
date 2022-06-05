using MessagePack;

namespace Warehouse.Shared.Packets;

[MessagePackObject(keyAsPropertyName: true)]
public class Packet : IPacket
{
    public ulong Identity { get; init; }
    public byte[] Buffer { get; init; }

    public Packet(ulong identity, byte[] buffer)
    {
        Identity = identity;
        Buffer = buffer;
    }
}
