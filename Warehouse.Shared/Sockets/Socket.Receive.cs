using MessagePack;
using System.Net;
using System.Net.Sockets;
using Warehouse.Shared.Packets;

namespace Warehouse.Shared.Sockets;

public partial class Socket : ISocket
{
	private void ReceiveCallback(IAsyncResult ar)
	{
		var bytes = socket.EndReceive(ar, out var errorCode);
		((TaskCompletionSource<ISocketOperationResult>)ar.AsyncState!).SetResult(
			new SocketOperationResult(bytes, errorCode)
		);
	}

	public Task<ISocketOperationResult> Receive(IPacketHeader packet)
	{
		var buffer = MessagePackSerializer.Serialize(packet);
		return Receive(buffer, 0, buffer.Length, SocketFlags.None);
	}

	public Task<ISocketOperationResult> Receive(byte[] buffer)
	{
		return Receive(buffer, 0, buffer.Length, SocketFlags.None);
	}

	public Task<ISocketOperationResult> Receive(byte[] buffer, int offset, int size)
	{
		return Receive(buffer, offset, size, SocketFlags.None);
	}

	public Task<ISocketOperationResult> Receive(
		byte[] buffer,
		int offset,
		int size,
		SocketFlags socketFlags
	)
	{
		var taskCompletionSource = new TaskCompletionSource<ISocketOperationResult>();
		socket.BeginReceive(
			buffer,
			offset,
			size,
			socketFlags,
			new AsyncCallback(ReceiveCallback),
			taskCompletionSource
		);
		return taskCompletionSource.Task;
	}

	public IAsyncResult BeginReceive(
		IPacketHeader packet,
		Action<IAsyncResult> callback,
		SocketFlags socketFlags = SocketFlags.None,
		object? state = null
	)
	{
		var buffer = MessagePackSerializer.Serialize(packet);
		return socket.BeginReceive(
			buffer,
			0,
			buffer.Length,
			socketFlags,
			new AsyncCallback(callback),
			state
		);
	}

	public IAsyncResult BeginReceive(
		byte[] buffer,
		Action<IAsyncResult> callback,
		int offset = 0,
		int? size = null,
		SocketFlags socketFlags = SocketFlags.None,
		object? state = null
	)
	{
		size ??= buffer.Length;
		return socket.BeginReceive(
			buffer,
			offset,
			(int)size,
			socketFlags,
			new AsyncCallback(callback),
			state
		);
	}

	public int EndReceive(IAsyncResult asyncResult)
	{
		return socket.EndReceive(asyncResult);
	}

	public int EndReceive(IAsyncResult asyncResult, out SocketError socketError)
	{
		return socket.EndReceive(asyncResult, out socketError);
	}
}
