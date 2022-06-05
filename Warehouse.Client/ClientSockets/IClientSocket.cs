namespace Warehouse.Client.ClientSockets;

public interface IClientSocket
{
    Task<bool> Connect(string hostname, int port);
}
