namespace Warehouse.Server.SocketClients;

public partial class SocketClient : IDisposable
{
    public event Action<ISocketClient>? DisconnectEvent;

    public void Disconnect()
    {
        if (DisconnectEvent is not null)
        {
            DisconnectEvent(this);
        }
        Dispose();
    }
}
