using Warehouse.Shared.Sockets;
using Warehouse.Shared.Packets;

namespace Warehouse.Server.SocketClients;

public interface ISocketClient : ISocket
{
    void BeginReceive();
    event Action<ISocketClient>? Disconnecting;
    event Action<ISocketClient, IPacket>? Received;
}
