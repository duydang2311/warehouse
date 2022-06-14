using MessagePack;
namespace Warehouse.Shared.Packets;

[MessagePackObject(true)]
public class AuthenticationResponsePacket : IAuthenticationResponsePacket
{
	public bool Ok { get; private set; }
	public AuthenticationResponsePacket(bool ok)
	{
		Ok = ok;
	}
}

