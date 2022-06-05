using System.Net;
using System.Net.Sockets;

namespace Warehouse.Shared.Sockets;

public partial class Socket : ISocket
{
    public Task<bool> Connect(EndPoint remoteEP)
    {
        var taskCompletionSource = new TaskCompletionSource<bool>();
        InternalSocket.BeginConnect(remoteEP, ConnectCallback, taskCompletionSource);
        return taskCompletionSource.Task;
    }

    private void ConnectCallback(IAsyncResult ar)
    {
        try
        {
            InternalSocket.EndConnect(ar);
            ((TaskCompletionSource<bool>)ar.AsyncState!).SetResult(true);
        }
        catch
        {
            ((TaskCompletionSource<bool>)ar.AsyncState!).SetResult(false);
        }
    }
}
