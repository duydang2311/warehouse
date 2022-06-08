using MessagePack;
using Warehouse.Shared.Packets.Identifiers;

namespace Warehouse.Shared.Packets.Serializers;

public class PacketDataSerializer : IPacketDataSerializer
{
    private readonly IPacketDataIdentifier identifier;
    private readonly IPacketHeaderFactory PacketHeaderFactory;

    public PacketDataSerializer(
        IPacketDataIdentifier identifier,
        IPacketHeaderFactory PacketHeaderFactory
    )
    {
        this.identifier = identifier;
        this.PacketHeaderFactory = PacketHeaderFactory;
    }

    public IPacketHeader? TrySerialize<T>(T packetData) where T : IPacketData
    {
        try
        {
            return PacketHeaderFactory.GetService(
                identifier.TryIdentify<T>(),
                MessagePackSerializer.Serialize<T>(packetData)
            );
        }
        catch
        {
            return default(IPacketHeader?);
        }
    }

    public async Task<IPacketHeader?> TrySerializeAsync<T>(T packetData) where T : IPacketData
    {
        try
        {
            using var stream = new MemoryStream();
            await MessagePackSerializer.SerializeAsync<T>(stream, packetData);
            return PacketHeaderFactory.GetService(identifier.TryIdentify<T>(), stream.ToArray());
        }
        catch
        {
            return default(IPacketHeader?);
        }
    }

    public T? TryDeserialize<T>(IPacketHeader packet) where T : IPacketData
    {
        try
        {
            return MessagePackSerializer.Deserialize<T>(packet.Buffer);
        }
        catch
        {
            return default(T?);
        }
    }

    public async Task<T?> TryDeserializeAsync<T>(IPacketHeader packet) where T : IPacketData
    {
        try
        {
            using var stream = new MemoryStream(packet.Buffer);
            return await MessagePackSerializer.DeserializeAsync<T>(stream);
        }
        catch
        {
            return default(T?);
        }
    }

    public async Task<IPacketHeader?> TryDeserializeAsync(Stream stream)
    {
        try
        {
            return await MessagePackSerializer.DeserializeAsync<IPacketHeader>(stream);
        }
        catch
        {
            return default(IPacketHeader?);
        }
    }
}
