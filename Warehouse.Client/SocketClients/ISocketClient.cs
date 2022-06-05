using System.Net.Sockets;

namespace Warehouse.Client.SocketClients;

using Warehouse.Shared.Sockets;

public interface ISocketClient
{
    Task<bool> Connect(string hostname, int port);
}
