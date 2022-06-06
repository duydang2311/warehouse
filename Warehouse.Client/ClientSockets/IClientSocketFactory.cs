using Warehouse.Shared.Services;
using Warehouse.Shared.Sockets;

namespace Warehouse.Client.ClientSockets;

public interface IClientSocketFactory : IServiceFactory<IClientSocket>
{
    IClientSocket GetService(System.Net.Sockets.Socket socket);
}
