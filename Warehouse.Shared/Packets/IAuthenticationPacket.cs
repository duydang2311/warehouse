using MessagePack;
namespace Warehouse.Shared.Packets;

[Union(0, typeof(AuthenticationPacket))]
public interface IAuthenticationPacket : IPacket
{
	string Username { get; }
	string Password { get; }
}

