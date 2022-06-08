namespace Warehouse.Shared.Packets.Serializers;

public interface IPacketDataSerializer
{
    IPacketHeader? TrySerialize<T>(T packetData) where T : IPacketData;
    Task<IPacketHeader?> TrySerializeAsync<T>(T packetData) where T : IPacketData;
    T? TryDeserialize<T>(IPacketHeader packet) where T : IPacketData;
    Task<T?> TryDeserializeAsync<T>(IPacketHeader packet) where T : IPacketData;
    Task<IPacketHeader?> TryDeserializeAsync(Stream stream);
}
