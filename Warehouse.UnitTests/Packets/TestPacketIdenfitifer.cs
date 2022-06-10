using System.Reflection;
using Warehouse.Shared.Packets;
using Warehouse.Shared.Packets.Identifiers;
using Warehouse.Shared.Packets.Serializers;

namespace Warehouse.UnitTests.Packets;

public class TestPacketIdentifier
{
	private readonly IPacketIdentifier identifier;
	private readonly IPacketSerializer serializer;

	public TestPacketIdentifier()
	{
		identifier = new PacketIdentifier();
		serializer = new PacketSerializer(identifier, new PacketHeaderFactory());
		identifier.Register(Assembly.GetExecutingAssembly());
		foreach (var i in Assembly.GetExecutingAssembly().GetReferencedAssemblies())
		{
			if (i.Name == "Warehouse.Shared")
			{
				identifier.Register(Assembly.Load(i));
				break;
			}
		}
	}

	[Fact]
	public void Identity_Not_Equal_0()
	{
		var data = new ClientPacket(ClientPacketType.Select);
		var packet = serializer.TrySerialize<IClientPacket>(data);
		Assert.NotNull(packet);
		Assert.NotEqual((ulong)0, identifier.TryIdentify<IClientPacket>());
	}

	[Fact]
	public void Serialized_Identity_Must_Equal()
	{
		var data = new ClientPacket(ClientPacketType.Select);
		var packet = serializer.TrySerialize<IClientPacket>(data);
		Assert.NotNull(packet);
		Assert.Equal(packet!.Identity, identifier.TryIdentify<IClientPacket>());
	}
}
