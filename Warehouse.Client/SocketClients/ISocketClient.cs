using System.Net.Sockets;

namespace Warehouse.Client.SocketClients;

public interface ISocketClient
{
    Socket? Connect(string hostname, int port);
}
