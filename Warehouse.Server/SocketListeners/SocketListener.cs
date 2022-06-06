using Warehouse.Server.SocketClients;

namespace Warehouse.Server.SocketListeners;

using Warehouse.Shared.Sockets;

public partial class SocketListener : Socket, ISocketListener
{
    public List<ISocketClient> Clients { get; } = new List<ISocketClient>();

    public SocketListener(System.Net.Sockets.Socket socket) : base(socket) { }
}
