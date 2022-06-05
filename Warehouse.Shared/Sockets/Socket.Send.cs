using System.Net;
using System.Net.Sockets;

namespace Warehouse.Shared.Sockets;

public partial class Socket : ISocket
{
    public Task<ISocketOperationResult> Send(byte[] bytes)
    {
        return Send(bytes, 0, bytes.Length, SocketFlags.None);
    }

    public Task<ISocketOperationResult> Send(byte[] bytes, int offset, int size)
    {
        return Send(bytes, offset, size, SocketFlags.None);
    }

    public Task<ISocketOperationResult> Send(
        byte[] bytes,
        int offset,
        int size,
        SocketFlags socketFlags
    )
    {
        var taskCompletionSource = new TaskCompletionSource<ISocketOperationResult>();
        InternalSocket.BeginSend(
            bytes,
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
        var bytes = InternalSocket.EndSend(ar, out var errorCode);
        ((TaskCompletionSource<ISocketOperationResult>)ar.AsyncState!).SetResult(
            new SocketOperationResult() { Bytes = 1, ErrorCode = SocketError.Success }
        );
    }
}
