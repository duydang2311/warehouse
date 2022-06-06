using Warehouse.Shared.Sockets;
using Warehouse.Server.SocketClients;

namespace Warehouse.Server.SocketListeners;

public partial class SocketListener : Socket, ISocketListener
{
    public void BeginAccept()
    {
        BeginAccept(AcceptCallback);
    }

    private void AcceptCallback(IAsyncResult ar)
    {
        var handler = EndAccept(ar);
        var client = socketClientFactory.GetService(handler);
        client.BeginReceive();
        Clients.Add(client);
        Console.WriteLine($"New connection accepted");
        BeginAccept(AcceptCallback);
    }
}
