using Warehouse.Shared.Packets;
using Warehouse.Shared.Packets.Identifiers;
using Warehouse.Shared.Packets.Serializers;

namespace Warehouse.UnitTests.Packets;

public class TestPacketDataSerializer
{
    private readonly IPacketDataIdentifier identifier;
    private readonly IPacketDataSerializer serializer;

    public TestPacketDataSerializer()
    {
        identifier = new PacketDataIdentifier();
        serializer = new PacketDataSerializer(identifier, new PacketFactory());
    }

    [Fact]
    public void Serialize_Deserialize_Should_Equal()
    {
        var data = new ClientPacketData(ClientPacketType.Select);
        var packet = serializer.TrySerialize<IClientPacketData>(data);
        Assert.NotNull(packet);
        var deserialized = serializer.TryDeserialize<IClientPacketData>(packet!);
        Assert.NotNull(deserialized);
        Assert.Equal(data.Type, deserialized!.Type);
    }
}
