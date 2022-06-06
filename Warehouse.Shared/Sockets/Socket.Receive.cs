using MessagePack;
using System.Net;
using System.Net.Sockets;
using Warehouse.Shared.Packets;

namespace Warehouse.Shared.Sockets;

public partial class Socket : ISocket
{
    public Task<ISocketOperationResult> Receive(IPacket packet)
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

    private void ReceiveCallback(IAsyncResult ar)
    {
        var bytes = socket.EndReceive(ar, out var errorCode);
        ((TaskCompletionSource<ISocketOperationResult>)ar.AsyncState!).SetResult(
            new SocketOperationResult(bytes, errorCode)
        );
    }
}
