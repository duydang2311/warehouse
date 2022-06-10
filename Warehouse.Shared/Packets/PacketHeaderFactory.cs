namespace Warehouse.Shared.Packets;

public class PacketHeaderFactory : IPacketHeaderFactory
{
	public IPacketHeader GetService(ulong identity, byte[] buffer) =>
		new PacketHeader(identity, buffer);
}
