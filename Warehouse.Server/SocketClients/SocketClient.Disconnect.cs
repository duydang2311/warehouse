namespace Warehouse.Server.SocketClients;

public partial class SocketClient : IDisposable
{
    public event Action<ISocketClient>? DisconnectedEvent;

    public void Disconnect()
    {
        Dispose();
        if (DisconnectedEvent is not null)
        {
            DisconnectedEvent(this);
        }
    }
}
