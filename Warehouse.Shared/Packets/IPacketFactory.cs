namespace Warehouse.Shared.Packets;

public interface IPacketFactory
{
	IAuthenticationPacket GetAuthenticationPacket(string username, string password);
	IAuthenticationResponsePacket GetAuthenticationResponsePacket(bool ok);
}
