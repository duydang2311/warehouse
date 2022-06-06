using System.Net;
using System.Net.Sockets;

namespace Warehouse.Shared.Sockets;

public partial class Socket : ISocket
{
    public void Shutdown(SocketShutdown how)
    {
        socket.Shutdown(how);
    }
}
