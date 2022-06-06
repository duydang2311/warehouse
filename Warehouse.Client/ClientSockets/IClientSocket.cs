using Warehouse.Shared.Sockets;

namespace Warehouse.Client.ClientSockets;

public interface IClientSocket
{
    ISocket? Socket { get; }
    Task<bool> Connect(string hostname, int port);
}
