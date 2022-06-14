using MessagePack;
namespace Warehouse.Shared.Packets;

[MessagePackObject(true)]
public class AuthenticationPacket : IAuthenticationPacket
{
	public string Username { get; private set; }
	public string Password { get; private set; }
	public AuthenticationPacket(string username, string password)
	{
		Username = username;
		Password = password;
	}
}

