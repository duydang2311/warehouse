namespace Warehouse.Shared.Sockets;

public partial class Socket : ISocket
{
	private void AcceptCallback(IAsyncResult ar)
	{
		var handler = socket.EndAccept(ar);
		((TaskCompletionSource<System.Net.Sockets.Socket>)ar.AsyncState!).SetResult(handler);
	}

	public Task<System.Net.Sockets.Socket> Accept()
	{
		var taskCompletionSource = new TaskCompletionSource<System.Net.Sockets.Socket>();
		socket.Listen(0);
		socket.BeginAccept(new AsyncCallback(AcceptCallback), taskCompletionSource);
		return taskCompletionSource.Task;
	}

	public IAsyncResult BeginAccept(Action<IAsyncResult> callback, object? state = null)
	{
		socket.Listen(0);
		return socket.BeginAccept(new AsyncCallback(callback), state);
	}

	public System.Net.Sockets.Socket EndAccept(out byte[] buffer, IAsyncResult asyncResult)
	{
		return socket.EndAccept(out buffer, asyncResult);
	}

	public System.Net.Sockets.Socket EndAccept(
		out byte[] buffer,
		out int bytesTransferred,
		IAsyncResult asyncResult
	)
	{
		return socket.EndAccept(out buffer, out bytesTransferred, asyncResult);
	}

	public System.Net.Sockets.Socket EndAccept(IAsyncResult asyncResult)
	{
		return socket.EndAccept(asyncResult);
	}
}
