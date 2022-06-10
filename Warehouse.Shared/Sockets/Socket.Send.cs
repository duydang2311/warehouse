using MessagePack;
using System.Net;
using System.Net.Sockets;
using Warehouse.Shared.Packets;

namespace Warehouse.Shared.Sockets;

public partial class Socket : ISocket
{
	private void SendCallback(IAsyncResult ar)
	{
		var bytes = socket.EndSend(ar, out var errorCode);
		((TaskCompletionSource<ISocketOperationResult>)ar.AsyncState!).SetResult(
			new SocketOperationResult(bytes, errorCode)
		);
	}

	public Task<ISocketOperationResult> Send(IPacketHeader packet)
	{
		var buffer = MessagePackSerializer.Serialize(packet);
		return Send(buffer, 0, buffer.Length, SocketFlags.None);
	}

	public Task<ISocketOperationResult> Send(byte[] buffer)
	{
		return Send(buffer, 0, buffer.Length, SocketFlags.None);
	}

	public Task<ISocketOperationResult> Send(byte[] buffer, int offset, int size)
	{
		return Send(buffer, offset, size, SocketFlags.None);
	}

	public Task<ISocketOperationResult> Send(
		byte[] buffer,
		int offset,
		int size,
		SocketFlags socketFlags
	)
	{
		var taskCompletionSource = new TaskCompletionSource<ISocketOperationResult>();
		socket.BeginSend(
			buffer,
			offset,
			size,
			socketFlags,
			new AsyncCallback(SendCallback),
			taskCompletionSource
		);
		return taskCompletionSource.Task;
	}

	public IAsyncResult BeginSend(
		IPacketHeader packet,
		Action<IAsyncResult> callback,
		SocketFlags socketFlags = SocketFlags.None,
		object? state = null
	)
	{
		var buffer = MessagePackSerializer.Serialize(packet);
		return socket.BeginSend(
			buffer,
			0,
			buffer.Length,
			socketFlags,
			new AsyncCallback(callback),
			state
		);
	}

	public IAsyncResult BeginSend(
		byte[] buffer,
		Action<IAsyncResult> callback,
		int offset = 0,
		int? size = null,
		SocketFlags socketFlags = SocketFlags.None,
		object? state = null
	)
	{
		size ??= buffer.Length;
		return socket.BeginSend(
			buffer,
			offset,
			(int)size,
			socketFlags,
			new AsyncCallback(callback),
			state
		);
	}

	public int EndSend(IAsyncResult asyncResult)
	{
		return socket.EndSend(asyncResult);
	}

	public int EndSend(IAsyncResult asyncResult, out SocketError socketError)
	{
		return socket.EndSend(asyncResult, out socketError);
	}
}
