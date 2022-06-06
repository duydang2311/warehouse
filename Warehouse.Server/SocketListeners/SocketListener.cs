using Warehouse.Server.SocketClients;

namespace Warehouse.Server.SocketListeners;

using Warehouse.Shared.Sockets;

public partial class SocketListener : Socket, ISocketListener
{
    private readonly ISocketClientFactory socketClientFactory;
    public List<ISocketClient> Clients { get; } = new List<ISocketClient>();

    public SocketListener(
        ISocketClientFactory socketClientFactory,
        System.Net.Sockets.Socket socket
    ) : base(socket)
    {
        this.socketClientFactory = socketClientFactory;
    }
}
