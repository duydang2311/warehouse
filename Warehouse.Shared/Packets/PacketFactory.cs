namespace Warehouse.Shared.Packets;

public class PacketFactory : IPacketFactory
{
	public IAuthenticationPacket GetAuthenticationPacket(string username, string password)
	{
		return new AuthenticationPacket(username, password);
	}
	public IAuthenticationResponsePacket GetAuthenticationResponsePacket(bool ok)
	{
		return new AuthenticationResponsePacket(ok);
	}
}
