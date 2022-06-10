using MessagePack;

namespace Warehouse.Shared.Packets;

[Union(0, typeof(PacketHeader))]
public interface IPacketHeader
{
	ulong Identity { get; }

	byte[] Buffer { get; }
}
