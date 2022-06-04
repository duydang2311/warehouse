using System.Net.Sockets;
using Warehouse.Server.SocketClients;
using Warehouse.Shared.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class ExtendServiceCollection
{
    public static IServiceCollection WithSocketClients(this IServiceCollection self)
    {
        self.AddSingleton<IServiceFactory<Socket, ISocketClient>, SocketClientFactory>();
        return self;
    }
}
