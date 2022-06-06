using Warehouse.Shared.Services;

namespace Warehouse.Server.SocketClients;

public interface ISocketClientFactory
    : IServiceFactory<System.Net.Sockets.Socket, ISocketClient> { }
