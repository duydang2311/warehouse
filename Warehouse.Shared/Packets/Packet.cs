using MessagePack;

namespace Warehouse.Shared.Packets;

[MessagePackObject(keyAsPropertyName: true)]
public class Packet : IPacket
{
    public ulong Identity { get; protected set; }

    public byte[] Buffer { get; protected set; }

    public Packet(ulong identity, byte[] buffer)
    {
        Identity = identity;
        Buffer = buffer;
    }
}
