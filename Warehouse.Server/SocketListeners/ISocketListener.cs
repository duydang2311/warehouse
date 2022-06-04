using Warehouse.Server.SocketClients;

namespace Warehouse.Server.SocketListeners;

public interface ISocketListener
{
    void Listen();
    List<ISocketClient> Clients { get; }
}
