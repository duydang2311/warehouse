using Microsoft.Extensions.DependencyInjection;
using System.Net.Sockets;
using Warehouse.Server.SocketHandlers;

namespace Warehouse.Server.SocketListeners;

public class SocketListenerFactory : ISocketListenerFactory
{
    private readonly IServiceProvider serviceProvider;

    public SocketListenerFactory(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public ISocketListener GetService(System.Net.Sockets.Socket socket) =>
        new SocketListener(serviceProvider.GetRequiredService<ISocketHandlerFactory>(), socket);

    public ISocketListener GetService() =>
        new SocketListener(
            serviceProvider.GetRequiredService<ISocketHandlerFactory>(),
            new System.Net.Sockets.Socket(
                AddressFamily.InterNetworkV6,
                SocketType.Stream,
                ProtocolType.Tcp
            )
        );
}
