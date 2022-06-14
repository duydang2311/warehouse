using MessagePack;
namespace Warehouse.Shared.Packets;

[Union(0, typeof(AuthenticationResponsePacket))]
public interface IAuthenticationResponsePacket : IPacket
{
	bool Ok { get; }
}

