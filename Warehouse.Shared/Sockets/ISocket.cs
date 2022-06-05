using System.Net.Sockets;

namespace Warehouse.Shared.Sockets;

public interface ISocket
{
    System.Net.Sockets.Socket InternalSocket { get; }
    Task<ISocketOperationResult> Send(byte[] bytes);
    Task<ISocketOperationResult> Send(byte[] bytes, int offset, int size);
    Task<ISocketOperationResult> Send(byte[] bytes, int offset, int size, SocketFlags socketFlags);
}
