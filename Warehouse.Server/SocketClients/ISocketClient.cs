using Warehouse.Shared.Sockets;

namespace Warehouse.Server.SocketClients;

public interface ISocketClient : ISocket
{
    void BeginReceive();
    event Action<ISocketClient> Disconnecting;
}
