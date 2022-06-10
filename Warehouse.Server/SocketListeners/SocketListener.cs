using Warehouse.Server.SocketHandlers;

namespace Warehouse.Server.SocketListeners;

using Warehouse.Shared.Sockets;

public partial class SocketListener : Socket, ISocketListener
{
	private readonly ISocketHandlerFactory SocketHandlerFactory;
	public List<ISocketHandler> Clients { get; } = new List<ISocketHandler>();

	public SocketListener(
		ISocketHandlerFactory SocketHandlerFactory,
		System.Net.Sockets.Socket socket
	) : base(socket)
	{
		this.SocketHandlerFactory = SocketHandlerFactory;
	}
}
