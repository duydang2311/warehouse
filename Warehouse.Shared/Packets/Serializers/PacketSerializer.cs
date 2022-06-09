using MessagePack;
using Warehouse.Shared.Packets.Identifiers;

namespace Warehouse.Shared.Packets.Serializers;

public class PacketSerializer : IPacketSerializer
{
    private readonly IPacketIdentifier identifier;
    private readonly IPacketHeaderFactory packetHeaderFactory;

    public PacketSerializer(IPacketIdentifier identifier, IPacketHeaderFactory packetHeaderFactory)
    {
        this.identifier = identifier;
        this.packetHeaderFactory = packetHeaderFactory;
    }

    public IPacketHeader? TrySerialize<T>(T packet) where T : IPacket
    {
        try
        {
            return packetHeaderFactory.GetService(
                identifier.TryIdentify<T>(),
                MessagePackSerializer.Serialize<T>(packet)
            );
        }
        catch
        {
            return default(IPacketHeader?);
        }
    }

    public async Task<IPacketHeader?> TrySerializeAsync<T>(T packet) where T : IPacket
    {
        try
        {
            using var stream = new MemoryStream();
            await MessagePackSerializer.SerializeAsync<T>(stream, packet);
            return packetHeaderFactory.GetService(identifier.TryIdentify<T>(), stream.ToArray());
        }
        catch
        {
            return default(IPacketHeader?);
        }
    }

    public async Task<Stream?> TrySerializeAsync(IPacketHeader header)
    {
        try
        {
            var stream = new MemoryStream();
            await MessagePackSerializer.SerializeAsync(stream, header);
            return stream;
        }
        catch
        {
            return default(Stream?);
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
            stream.Position = 0;
            return await MessagePackSerializer.DeserializeAsync<IPacketHeader>(stream);
        }
        catch
        {
            return default(IPacketHeader?);
        }
    }

    public async Task<T?> TryDeserializeAsync<T>(Stream stream) where T : IPacket
    {
        try
        {
            stream.Position = 0;
            var header = await MessagePackSerializer.DeserializeAsync<IPacketHeader>(stream);
            using var bufferStream = new MemoryStream(header.Buffer);
            return await MessagePackSerializer.DeserializeAsync<T>(bufferStream);
        }
        catch
        {
#if DEBUG
            throw;
#else
            return default(T?);
#endif
        }
    }
}
