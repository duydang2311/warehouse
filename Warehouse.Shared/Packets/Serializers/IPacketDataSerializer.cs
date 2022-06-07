namespace Warehouse.Shared.Packets.Serializers;

public interface IPacketDataSerializer
{
    IPacket? TrySerialize<T>(T packetData) where T : IPacketData;
    T? TryDeserialize<T>(IPacket packet) where T : IPacketData;
}
