using System.Net;
using System.Net.Sockets;
using Warehouse.Shared.Packets;

namespace Warehouse.Shared.Sockets;

public interface ISocket
{
    System.Net.Sockets.Socket InternalSocket { get; }
    Task<bool> Connect(EndPoint remoteEP);
    Task<ISocketOperationResult> Send(IPacket packet);
    Task<ISocketOperationResult> Send(byte[] bytes);
    Task<ISocketOperationResult> Send(byte[] bytes, int offset, int size);
    Task<ISocketOperationResult> Send(byte[] bytes, int offset, int size, SocketFlags socketFlags);
}
