using Warehouse.Shared.Services;

namespace Warehouse.Server.SocketListeners;

public interface ISocketListenerFactory
    : IServiceFactory<System.Net.Sockets.Socket, ISocketListener>
{
    ISocketListener GetService();
}
