using MessagePack;
using System.Net;
using System.Net.Sockets;
using Warehouse.Shared.Packets;

namespace Warehouse.Shared.Sockets;

public partial class Socket : ISocket
{
    public Task<ISocketOperationResult> Send(IPacket packet)
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

    private void SendCallback(IAsyncResult ar)
    {
        var bytes = socket.EndSend(ar, out var errorCode);
        ((TaskCompletionSource<ISocketOperationResult>)ar.AsyncState!).SetResult(
            new SocketOperationResult(bytes, errorCode)
        );
    }
}
