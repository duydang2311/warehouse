using Warehouse.Shared.Sockets;

namespace Warehouse.Client.ClientSockets;

public partial class ClientSocket : Socket, IClientSocket
{
    public ClientSocket(System.Net.Sockets.Socket socket) : base(socket) { }
}
