using Warehouse.Shared.Sockets;

namespace Warehouse.Server.SocketClients;

public partial class SocketClient : Socket, ISocketClient
{
    public SocketClient(System.Net.Sockets.Socket socket) : base(socket) { }
}
