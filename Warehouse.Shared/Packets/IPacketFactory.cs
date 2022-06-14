namespace Warehouse.Shared.Packets;

public interface IPacketFactory
{
	T GetService<T>(string username, string password) where T : class, IAuthenticationPacket;
}
