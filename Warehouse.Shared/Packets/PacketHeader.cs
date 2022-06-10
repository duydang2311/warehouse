using MessagePack;

namespace Warehouse.Shared.Packets;

[MessagePackObject(keyAsPropertyName: true)]
public class PacketHeader : IPacketHeader
{
	public ulong Identity { get; protected set; }

	public byte[] Buffer { get; protected set; }

	public PacketHeader(ulong identity, byte[] buffer)
	{
		Identity = identity;
		Buffer = buffer;
	}
}
