using Warehouse.Shared.Packets;
using Warehouse.Shared.Packets.Identifiers;
using Warehouse.Shared.Packets.Serializers;

namespace Warehouse.UnitTests.Packets;

public class TestPacketSerializer
{
    private readonly IPacketIdentifier identifier;
    private readonly IPacketSerializer serializer;

    public TestPacketSerializer()
    {
        identifier = new PacketIdentifier();
        serializer = new PacketSerializer(identifier, new PacketHeaderFactory());
    }

    [Fact]
    public void Serialize_Deserialize_Should_Equal()
    {
        var data = new ClientPacket(ClientPacketType.Select);
        var packet = serializer.TrySerialize<IClientPacket>(data);
        Assert.NotNull(packet);
        var deserialized = serializer.TryDeserialize<IClientPacket>(packet!);
        Assert.NotNull(deserialized);
        Assert.Equal(data.Type, deserialized!.Type);
    }
}
