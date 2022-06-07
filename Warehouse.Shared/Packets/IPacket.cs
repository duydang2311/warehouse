using MessagePack;

namespace Warehouse.Shared.Packets;

[Union(0, typeof(Packet))]
public interface IPacket
{
    ulong Identity { get; }

    [IgnoreMember]
    byte[] Buffer { get; }
}
