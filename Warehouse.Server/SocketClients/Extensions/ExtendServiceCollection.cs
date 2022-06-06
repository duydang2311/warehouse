using Warehouse.Server.SocketClients;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class ExtendServiceCollection
{
    public static IServiceCollection WithSocketClients(this IServiceCollection self)
    {
        self.AddSingleton<ISocketClientFactory, SocketClientFactory>();
        return self;
    }
}
