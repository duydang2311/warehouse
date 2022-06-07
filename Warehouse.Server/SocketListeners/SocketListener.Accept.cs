using Warehouse.Shared.Sockets;
using Warehouse.Server.SocketClients;

namespace Warehouse.Server.SocketListeners;

public partial class SocketListener : Socket, ISocketListener
{
    public event Action<ISocketClient>? Accepted;

    public void BeginAccept()
    {
        BeginAccept(AcceptCallback);
    }

    private void AcceptCallback(IAsyncResult ar)
    {
        var handler = EndAccept(ar);
        var client = socketClientFactory.GetService(handler);
        Clients.Add(client);
        BeginAccept(AcceptCallback);
        if (Accepted is not null)
        {
            Accepted(client);
        }
    }
}
