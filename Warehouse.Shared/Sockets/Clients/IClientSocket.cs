using Warehouse.Shared.Sockets;
using Warehouse.Shared.Packets;

namespace Warehouse.Shared.Sockets.Clients;

public interface IClientSocket : ISocket
{
	void BeginReceive();
	event Action<IClientSocket, IPacketHeader>? Received;
	event Action<IClientSocket>? RemoteDisconnecting;
}
