using Warehouse.Shared.Packets.Serializers;

namespace Warehouse.Shared.TcpServers;

public interface ITcpServerFactory
{
	TcpServer GetService(string address, int port);
}
