using System.Net.Sockets;
using Warehouse.Shared.Services;

namespace Warehouse.Shared.Sockets;

public interface ISocketFactory : IServiceFactory<System.Net.Sockets.Socket, ISocket>
{
    public ISocket GetService();
}
