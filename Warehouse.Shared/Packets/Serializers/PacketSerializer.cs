using MessagePack;
using Warehouse.Shared.Packets.Identifiers;

namespace Warehouse.Shared.Packets.Serializers;

public class PacketSerializer : IPacketSerializer
{
    private readonly IPacketIdentifier identifier;
    private readonly IPacketHeaderFactory PacketHeaderFactory;

    public PacketSerializer(IPacketIdentifier identifier, IPacketHeaderFactory PacketHeaderFactory)
    {
        this.identifier = identifier;
        this.PacketHeaderFactory = PacketHeaderFactory;
    }

    public IPacketHeader? TrySerialize<T>(T Packet) where T : IPacket
    {
        try
        {
            return PacketHeaderFactory.GetService(
                identifier.TryIdentify<T>(),
                MessagePackSerializer.Serialize<T>(Packet)
            );
        }
        catch
        {
            return default(IPacketHeader?);
        }
    }

    public async Task<IPacketHeader?> TrySerializeAsync<T>(T Packet) where T : IPacket
    {
        try
        {
            using var stream = new MemoryStream();
            await MessagePackSerializer.SerializeAsync<T>(stream, Packet);
            return PacketHeaderFactory.GetService(identifier.TryIdentify<T>(), stream.ToArray());
        }
        catch
        {
            return default(IPacketHeader?);
        }
    }

    public T? TryDeserialize<T>(IPacketHeader packet) where T : IPacket
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

    public async Task<T?> TryDeserializeAsync<T>(IPacketHeader packet) where T : IPacket
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
