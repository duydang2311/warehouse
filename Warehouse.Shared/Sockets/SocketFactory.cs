using System.Net.Sockets;
using Warehouse.Shared.Services;

namespace Warehouse.Shared.Sockets;

public class SocketFactory : IServiceFactory<AddressFamily, SocketType, ProtocolType, ISocket>
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
