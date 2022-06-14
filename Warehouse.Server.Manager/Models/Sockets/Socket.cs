using System.Net.Sockets;

namespace Warehouse.Server.Manager.Models.Sockets;

public class Socket : Shared.Sockets.Socket, ISocket
{
	public Socket() : base(
		new System.Net.Sockets.Socket(
			AddressFamily.InterNetworkV6,
			SocketType.Stream,
			ProtocolType.Tcp
		)
	)
	{ }

	public Socket(System.Net.Sockets.Socket socket) : base(socket) { }
}
