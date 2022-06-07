namespace Warehouse.Server.SocketClients;

public partial class SocketClient : IDisposable
{
    public event Action<ISocketClient>? Disconnecting;

    public void Disconnect()
    {
        if (Disconnecting is not null)
        {
            Disconnecting(this);
        }
    }
}
