namespace Warehouse.Server.SocketHandlers;

public partial class SocketHandler : IDisposable
{
	public event Action<ISocketHandler>? Disconnecting;

	public void Disconnect()
	{
		if (Disconnecting is not null)
		{
			Disconnecting(this);
		}
	}
}
