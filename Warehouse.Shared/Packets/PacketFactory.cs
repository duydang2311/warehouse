namespace Warehouse.Shared.Packets;

public class PacketFactory : IPacketFactory
{
    public IPacket GetService(ulong identity, byte[] buffer) => new Packet(identity, buffer);
}
