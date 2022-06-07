namespace Warehouse.Shared.Packets.Serializers;

public interface IPacketDataSerializer
{
    IPacket? TrySerialize<T>(T packetData) where T : IPacketData;
    Task<IPacket?> TrySerializeAsync<T>(T packetData) where T : IPacketData;
    T? TryDeserialize<T>(IPacket packet) where T : IPacketData;
    Task<T?> TryDeserializeAsync<T>(IPacket packet) where T : IPacketData;
    Task<IPacket?> TryDeserializeAsync(Stream stream);
}
