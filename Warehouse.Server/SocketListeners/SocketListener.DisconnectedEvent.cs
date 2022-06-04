using Warehouse.Server.SocketClients;

namespace Warehouse.Server.SocketListeners;

public partial class SocketListener : ISocketListener
{
    private void SocketClient_DisconnectEvent(ISocketClient sender)
    {
        Console.WriteLine($"{sender.Client.RemoteEndPoint} disconnected");
    }
}
