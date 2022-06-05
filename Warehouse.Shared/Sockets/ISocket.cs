using System.Net.Sockets;
using Warehouse.Shared.Packets;

namespace Warehouse.Shared.Sockets;

public interface ISocket
{
    System.Net.Sockets.Socket InternalSocket { get; }
    Task<ISocketOperationResult> Send(IPacket packet);
    Task<ISocketOperationResult> Send(byte[] bytes);
    Task<ISocketOperationResult> Send(byte[] bytes, int offset, int size);
    Task<ISocketOperationResult> Send(byte[] bytes, int offset, int size, SocketFlags socketFlags);
}
