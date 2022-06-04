namespace Warehouse.Server.SocketListeners;

public partial class SocketListener : ISocketListener
{
    private bool acceptBegun = false;

    public void Listen()
    {
        if (acceptBegun)
        {
            return;
        }
        acceptBegun = true;
        Console.WriteLine("Listening for connections...");
        listener.Listen(0);
        listener.BeginAccept(new AsyncCallback(AcceptCallback), null);
    }

    private void AcceptCallback(IAsyncResult result)
    {
        var client = listener.EndAccept(result);
        var socketClient = socketClientFactory.GetService(client);
        socketClient.DisconnectEvent += SocketClient_DisconnectEvent;
        Clients.Add(socketClient);
        Console.WriteLine($"{client.RemoteEndPoint} connected");

        listener.Listen(0);
        listener.BeginAccept(new AsyncCallback(AcceptCallback), null);
    }
}
