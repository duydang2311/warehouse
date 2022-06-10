namespace Warehouse.Shared.Packets.Serializers;

public interface IPacketSerializer
{
	IPacketHeader? TrySerialize<T>(T Packet) where T : IPacket;
	Task<IPacketHeader?> TrySerializeAsync<T>(T Packet) where T : IPacket;
	Task<Stream?> TrySerializeAsync(IPacketHeader header);
	T? TryDeserialize<T>(IPacketHeader packet) where T : IPacket;
	Task<T?> TryDeserializeAsync<T>(IPacketHeader packet) where T : IPacket;
	Task<IPacketHeader?> TryDeserializeAsync(Stream stream);
	Task<T?> TryDeserializeAsync<T>(Stream stream) where T : IPacket;
}
