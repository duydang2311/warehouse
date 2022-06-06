using System.Net;

namespace Warehouse.Shared.Sockets;

public partial class Socket : ISocket
{
    public void Bind(EndPoint localEP) => socket.Bind(localEP);
}
