using Warehouse.Shared.Services;
using Warehouse.Shared.Sockets;

namespace Warehouse.Shared.Sockets.Clients;

public interface IClientSocketFactory : IServiceFactory<IClientSocket>
{
	IClientSocket GetService(System.Net.Sockets.Socket socket);
}
