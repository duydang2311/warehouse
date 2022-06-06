using System.Net.Sockets;

namespace Warehouse.Shared.Sockets;

public class SocketFactory : ISocketFactory
{
    public ISocket GetService(
        AddressFamily addressFamily,
        SocketType socketType,
        ProtocolType protocolType
    )
    {
        return new Socket(addressFamily, socketType, protocolType);
    }
}
