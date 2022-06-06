using Microsoft.Extensions.DependencyInjection;
using System.Net.Sockets;
using Warehouse.Server.SocketClients;

namespace Warehouse.Server.SocketListeners;

public class SocketListenerFactory : ISocketListenerFactory
{
    private readonly IServiceProvider serviceProvider;

    public SocketListenerFactory(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public ISocketListener GetService(System.Net.Sockets.Socket socket) =>
        new SocketListener(serviceProvider.GetRequiredService<ISocketClientFactory>(), socket);

    public ISocketListener GetService() =>
        new SocketListener(
            serviceProvider.GetRequiredService<ISocketClientFactory>(),
            new System.Net.Sockets.Socket(
                AddressFamily.InterNetworkV6,
                SocketType.Stream,
                ProtocolType.Tcp
            )
        );
}
