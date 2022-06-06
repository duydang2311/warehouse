namespace Warehouse.Shared.Sockets;

public partial class Socket : ISocket
{
    public Task<System.Net.Sockets.Socket> Accept()
    {
        var taskCompletionSource = new TaskCompletionSource<System.Net.Sockets.Socket>();
        socket.Listen(0);
        socket.BeginAccept(new AsyncCallback(AcceptCallback), taskCompletionSource);
        return taskCompletionSource.Task;
    }

    private void AcceptCallback(IAsyncResult ar)
    {
        var handler = socket.EndAccept(ar);
        ((TaskCompletionSource<System.Net.Sockets.Socket>)ar.AsyncState!).SetResult(handler);
    }
}
