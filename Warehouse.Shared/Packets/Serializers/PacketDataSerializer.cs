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
        return packetFactory.GetService(
            identifier.TryIdentify<T>(),
            MessagePackSerializer.Serialize<T>(packetData)
        );
    }

    public T? TryDeserialize<T>(IPacket packet) where T : IPacketData
    {
        return MessagePackSerializer.Deserialize<T>(packet.Buffer);
    }
}
