using System.Net;
using System.Net.Sockets;
using Warehouse.Shared.Packets;

namespace Warehouse.Shared.Sockets;

public partial interface ISocket : IDisposable
{
    bool Connected { get; }
    EndPoint? LocalEndPoint { get; }
    EndPoint? RemoteEndPoint { get; }
    Task<bool> Connect(EndPoint remoteEP);
    Task<bool> Connect(string hostname, int port);
    Task<bool> Disconnect(bool reuseSocket);
    Task<System.Net.Sockets.Socket> Accept();
    public IAsyncResult BeginAccept(Action<IAsyncResult> callback, object? state);

    public System.Net.Sockets.Socket EndAccept(out byte[] buffer, IAsyncResult asyncResult);

    public System.Net.Sockets.Socket EndAccept(
        out byte[] buffer,
        out int bytesTransferred,
        IAsyncResult asyncResult
    );

    public System.Net.Sockets.Socket EndAccept(IAsyncResult asyncResult);
    Task<ISocketOperationResult> Send(IPacket packet);
    Task<ISocketOperationResult> Send(byte[] bytes);
    Task<ISocketOperationResult> Send(byte[] bytes, int offset, int size);
    Task<ISocketOperationResult> Send(byte[] bytes, int offset, int size, SocketFlags socketFlags);
    public IAsyncResult BeginSend(
        IPacket packet,
        Action<IAsyncResult> callback,
        SocketFlags socketFlags,
        object? state
    );
    public IAsyncResult BeginSend(
        byte[] buffer,
        Action<IAsyncResult> callback,
        int offset,
        int? size,
        SocketFlags socketFlags,
        object? state
    );
    int EndSend(IAsyncResult asyncResult);

    int EndSend(IAsyncResult asyncResult, out SocketError socketError);
    Task<ISocketOperationResult> Receive(byte[] bytes);
    Task<ISocketOperationResult> Receive(byte[] bytes, int offset, int size);
    Task<ISocketOperationResult> Receive(
        byte[] bytes,
        int offset,
        int size,
        SocketFlags socketFlags
    );
    IAsyncResult BeginReceive(
        IPacket packet,
        Action<IAsyncResult> callback,
        SocketFlags socketFlags,
        object? state
    );
    IAsyncResult BeginReceive(
        byte[] buffer,
        Action<IAsyncResult> callback,
        int offset,
        int? size,
        SocketFlags socketFlags,
        object? state
    );
    int EndReceive(IAsyncResult asyncResult);

    int EndReceive(IAsyncResult asyncResult, out SocketError socketError);
    void Bind(string hostname, int port);
    void Bind(EndPoint localEP);
    void Shutdown(SocketShutdown how);
}
