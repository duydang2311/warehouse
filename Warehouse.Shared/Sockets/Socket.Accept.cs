namespace Warehouse.Shared.Sockets;

public partial class Socket : ISocket
{
    public Task<ISocket> Accept()
    {
        var taskCompletionSource = new TaskCompletionSource<ISocket>();
        socket.Listen(0);
        socket.BeginAccept(new AsyncCallback(AcceptCallback), taskCompletionSource);
        return taskCompletionSource.Task;
    }

    private void AcceptCallback(IAsyncResult ar)
    {
        var handler = socket.EndAccept(ar);
        ((TaskCompletionSource<ISocket>)ar.AsyncState!).SetResult(new Socket(handler));
    }
}
