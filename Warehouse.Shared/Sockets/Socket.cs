using System.Net.Sockets;

namespace Warehouse.Shared.Sockets;

public partial class Socket : ISocket
{
    public System.Net.Sockets.Socket InternalSocket { get; private set; }

    public Socket(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType)
    {
        InternalSocket = new System.Net.Sockets.Socket(addressFamily, socketType, protocolType);
    }
}
