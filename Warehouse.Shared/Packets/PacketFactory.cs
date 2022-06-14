namespace Warehouse.Shared.Packets;

public class PacketFactory : IPacketFactory
{
	public T GetService<T>(string username, string password) where T : class, IAuthenticationPacket
	{
		return (new AuthenticationPacket(username, password) as T)!;
	}
}
