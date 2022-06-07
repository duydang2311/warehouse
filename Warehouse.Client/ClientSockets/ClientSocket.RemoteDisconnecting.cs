namespace Warehouse.Client.ClientSockets;

public partial class ClientSocket
{
    public event Action<IClientSocket>? RemoteDisconnecting;

    private void RemoteDisconnect()
    {
        if (RemoteDisconnecting is not null)
        {
            RemoteDisconnecting(this);
        }
    }
}
