using Warehouse.Shared.Services;

namespace Warehouse.Server.SocketHandlers;

public interface ISocketHandlerFactory
    : IServiceFactory<System.Net.Sockets.Socket, ISocketHandler> { }
