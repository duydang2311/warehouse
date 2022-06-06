using System.Net;
using System.Net.Sockets;
using Warehouse.Shared.Packets;

namespace Warehouse.Shared.Sockets;

public partial interface ISocket : IDisposable
{
    Task<bool> Connect(EndPoint remoteEP);
    Task<bool> Disconnect(bool reuseSocket);
    Task<ISocket> Accept();
    Task<ISocketOperationResult> Send(IPacket packet);
    Task<ISocketOperationResult> Send(byte[] bytes);
    Task<ISocketOperationResult> Send(byte[] bytes, int offset, int size);
    Task<ISocketOperationResult> Send(byte[] bytes, int offset, int size, SocketFlags socketFlags);
    void Bind(string hostname, int port);
    void Bind(EndPoint localEP);
}
