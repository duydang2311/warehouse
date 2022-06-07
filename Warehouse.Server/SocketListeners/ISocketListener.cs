using System.Net;
using Warehouse.Server.SocketClients;

namespace Warehouse.Server.SocketListeners;

using Warehouse.Shared.Sockets;

public interface ISocketListener : ISocket
{
    List<ISocketClient> Clients { get; }
    void BeginAccept();
    event Action<ISocketClient>? Accepted;
}
