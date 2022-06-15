using Warehouse.Shared.Packets.Serializers;

namespace Warehouse.Shared.TcpServers;

public class TcpServerFactory : ITcpServerFactory
{
	private readonly IPacketSerializer packetSerializer;
	public TcpServerFactory(IPacketSerializer packetSerializer)
	{
		this.packetSerializer = packetSerializer;
	}
	public TcpServer GetService(string address, int port)
		=> new(packetSerializer, address, port);
}
