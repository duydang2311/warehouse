using Warehouse.Shared.Packets.Serializers;

namespace Warehouse.Shared.TcpClients;

public class TcpClientFactory : ITcpClientFactory
{
	private TcpClient? instance = null;
	private readonly IPacketSerializer packetSerializer;
	public TcpClientFactory(IPacketSerializer packetSerializer)
	{
		this.packetSerializer = packetSerializer;
	}
	public TcpClient GetService(string address, int port)
	{
		if (instance is null)
		{
			instance = new TcpClient(packetSerializer, address, port);
		}
		return instance;
	}
	public TcpClient? GetService()
		=> instance;
}
