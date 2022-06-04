using System.Net.Sockets;
using Warehouse.Shared.Services;
using Warehouse.Server.SocketListeners;
using Warehouse.Server.SocketClients;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class ExtendServiceCollection
{
    public static IServiceCollection WithSocketListeners(
        this IServiceCollection self,
        string hostname,
        int port
    )
    {
        self.AddSingleton<SocketListener>(
            provider =>
                new SocketListener(
                    provider.GetRequiredService<IServiceFactory<Socket, ISocketClient>>(),
                    hostname,
                    port
                )
        );
        return self;
    }
}
