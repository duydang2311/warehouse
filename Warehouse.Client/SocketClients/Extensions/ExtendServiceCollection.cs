using Warehouse.Client.SocketClients;

namespace Microsoft.Extensions.DependencyInjection;

public static class ExtendServiceCollection
{
    public static IServiceCollection WithSocketClients(this IServiceCollection self)
    {
        self.AddSingleton<ISocketClient, SocketClient>();
        return self;
    }
}
