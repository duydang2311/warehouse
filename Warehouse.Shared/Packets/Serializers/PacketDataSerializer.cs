using MessagePack;
using Warehouse.Shared.Packets.Identifiers;

namespace Warehouse.Shared.Packets.Serializers;

public class PacketDataSerializer : IPacketDataSerializer
{
    private readonly IPacketDataIdentifier identifier;
    private readonly IPacketFactory packetFactory;

    public PacketDataSerializer(IPacketDataIdentifier identifier, IPacketFactory packetFactory)
    {
        this.identifier = identifier;
        this.packetFactory = packetFactory;
    }

    public IPacket? TrySerialize<T>(T packetData) where T : IPacketData
    {
        try
        {
            return packetFactory.GetService(
                identifier.TryIdentify<T>(),
                MessagePackSerializer.Serialize<T>(packetData)
            );
        }
        catch
        {
            return default(IPacket?);
        }
    }

    public async Task<IPacket?> TrySerializeAsync<T>(T packetData) where T : IPacketData
    {
        try
        {
            using var stream = new MemoryStream();
            await MessagePackSerializer.SerializeAsync<T>(stream, packetData);
            return packetFactory.GetService(identifier.TryIdentify<T>(), stream.ToArray());
        }
        catch
        {
            return default(IPacket?);
        }
    }

    public T? TryDeserialize<T>(IPacket packet) where T : IPacketData
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

    public async Task<T?> TryDeserializeAsync<T>(IPacket packet) where T : IPacketData
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
}
