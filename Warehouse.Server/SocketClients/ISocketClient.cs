namespace Warehouse.Server.SocketClients;

public interface ISocketClient : IDisposable
{
    event Action<ISocketClient>? DisconnectedEvent;
    void Disconnect();
}
