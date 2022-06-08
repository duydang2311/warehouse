using System.Net;
using Warehouse.Server.SocketHandlers;

namespace Warehouse.Server.SocketListeners;

using Warehouse.Shared.Sockets;

public interface ISocketListener : ISocket
{
    List<ISocketHandler> Clients { get; }
    void BeginAccept();
    event Action<ISocketHandler>? Accepted;
}
