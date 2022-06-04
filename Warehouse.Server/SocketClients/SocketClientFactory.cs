using System.Net.Sockets;
using Warehouse.Shared.Services;

namespace Warehouse.Server.SocketClients;

public class SocketClientFactory : IServiceFactory<Socket, ISocketClient>
{
    public ISocketClient GetService(Socket socket)
    {
        return new SocketClient(socket);
    }
}
