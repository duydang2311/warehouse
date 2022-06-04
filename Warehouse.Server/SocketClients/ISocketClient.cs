using System.Net.Sockets;

namespace Warehouse.Server.SocketClients;

public interface ISocketClient : IDisposable
{
    Socket Client { get; }
    event Action<ISocketClient>? DisconnectedEvent;
    void Disconnect();
}
