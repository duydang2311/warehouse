using System.Net;

namespace Warehouse.Shared.Sockets;

public partial class Socket : ISocket
{
	public Task<bool> Connect(EndPoint remoteEP)
	{
		var taskCompletionSource = new TaskCompletionSource<bool>();
		socket.BeginConnect(remoteEP, ConnectCallback, taskCompletionSource);
		return taskCompletionSource.Task;
	}

	public Task<bool> Connect(string hostname, int port)
	{
		var taskCompletionSource = new TaskCompletionSource<bool>();
		var host = Dns.GetHostEntry(hostname);
		var ipAddress = host.AddressList[0];
		var remoteEP = new IPEndPoint(ipAddress, port);
		socket.BeginConnect(remoteEP, ConnectCallback, taskCompletionSource);
		return taskCompletionSource.Task;
	}

	private void ConnectCallback(IAsyncResult ar)
	{
		try
		{
			socket.EndConnect(ar);
			((TaskCompletionSource<bool>)ar.AsyncState!).SetResult(true);
		}
		catch
		{
			((TaskCompletionSource<bool>)ar.AsyncState!).SetResult(false);
		}
	}
}
