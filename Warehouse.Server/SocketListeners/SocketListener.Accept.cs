using Warehouse.Shared.Sockets;
using Warehouse.Server.SocketHandlers;

namespace Warehouse.Server.SocketListeners;

public partial class SocketListener : Socket, ISocketListener
{
	public event Action<ISocketListener, ISocketHandler>? Accepted;

	public void BeginAccept()
	{
		BeginAccept(AcceptCallback);
	}

	private void AcceptCallback(IAsyncResult ar)
	{
		var handler = EndAccept(ar);
		var client = SocketHandlerFactory.GetService(handler);
		client.Disconnecting += Client_Disconnecting;
		Clients.Add(client);
		BeginAccept(AcceptCallback);
		if (Accepted is not null)
		{
			Accepted(this, client);
		}
	}

	private void Client_Disconnecting(ISocketHandler client)
	{
		Clients.Remove(client);
	}
}
