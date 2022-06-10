namespace Warehouse.Shared.Sockets;

public partial class Socket : ISocket
{
	public Task<bool> Disconnect(bool reuseSocket)
	{
		var taskCompletionSource = new TaskCompletionSource<bool>();
		socket.BeginDisconnect(reuseSocket, DisconnectCallback, taskCompletionSource);
		return taskCompletionSource.Task;
	}

	private void DisconnectCallback(IAsyncResult ar)
	{
		try
		{
			socket.EndDisconnect(ar);
			((TaskCompletionSource<bool>)ar.AsyncState!).SetResult(true);
		}
		catch
		{
			((TaskCompletionSource<bool>)ar.AsyncState!).SetResult(false);
		}
	}
}
