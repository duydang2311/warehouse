using System.Net;

namespace Warehouse.Shared.TcpClients;

public interface ITcpClientFactory
{
	TcpClient GetService(string address, int port);
	TcpClient? GetService();
}
