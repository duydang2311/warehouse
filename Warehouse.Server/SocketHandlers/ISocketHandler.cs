using Warehouse.Shared.Sockets;
using Warehouse.Shared.Packets;

namespace Warehouse.Server.SocketHandlers;

public interface ISocketHandler : ISocket
{
	void BeginReceive();
	event Action<ISocketHandler>? Disconnecting;
	event Action<ISocketHandler, IPacketHeader>? Received;
}
