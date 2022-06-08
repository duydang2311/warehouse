using Warehouse.Shared.Packets;
using Warehouse.Shared.Packets.Identifiers;
using Warehouse.Shared.Packets.Serializers;

namespace Warehouse.UnitTests.Packets;

public class TestPacketDataIdentifier
{
    private readonly IPacketDataIdentifier identifier;
    private readonly IPacketDataSerializer serializer;

    public TestPacketDataIdentifier()
    {
        identifier = new PacketDataIdentifier();
        serializer = new PacketDataSerializer(identifier, new PacketHeaderFactory());
    }

    [Fact]
    public void Identity_Not_Equal_0()
    {
        var data = new ClientPacketData(ClientPacketType.Select);
        var packet = serializer.TrySerialize<IClientPacketData>(data);
        Assert.NotNull(packet);
        Assert.NotEqual((ulong)0, identifier.TryIdentify<IClientPacketData>());
    }

    [Fact]
    public void Serialized_Identity_Must_Equal()
    {
        var data = new ClientPacketData(ClientPacketType.Select);
        var packet = serializer.TrySerialize<IClientPacketData>(data);
        Assert.NotNull(packet);
        Assert.Equal(packet!.Identity, identifier.TryIdentify<IClientPacketData>());
    }
}
