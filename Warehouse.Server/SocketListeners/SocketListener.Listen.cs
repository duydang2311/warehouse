namespace Warehouse.Server.SocketListeners;

public sealed partial class SocketListener
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
        Clients.Add(socketClientFactory.GetService(client));
        Console.WriteLine($"{client.RemoteEndPoint} connected");

        listener.Listen(0);
        listener.BeginAccept(new AsyncCallback(AcceptCallback), null);
    }
}
