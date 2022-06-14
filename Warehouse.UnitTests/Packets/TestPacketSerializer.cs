using System.Reflection;
using Warehouse.Shared.Packets;
using Warehouse.Shared.Packets.Identifiers;
using Warehouse.Shared.Packets.Serializers;
using MessagePack;

namespace Warehouse.UnitTests.Packets;

[Union(0, typeof(TestPacket))]
public interface ITestPacket : IPacket
{
	string Name { get; set; }
}

[MessagePackObject(true)]
public class TestPacket : ITestPacket
{
	public string Name { get; set; } = "Test packet";
}

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

	[Fact]
	public async Task Deserialize_To_IPacket_Type_Async()
	{
		var packet = new TestPacket();
		var header = await serializer.TrySerializeAsync<ITestPacket>(packet);
		Assert.NotNull(header);
		using var stream = await serializer.TrySerializeAsync(header!);
		Assert.NotNull(stream);
		var deserializedPacket = await serializer.TryDeserializeAsync<ITestPacket>(stream!);
		Assert.NotNull(deserializedPacket);
		Assert.Equal(packet!.Name, deserializedPacket!.Name);
	}
}
