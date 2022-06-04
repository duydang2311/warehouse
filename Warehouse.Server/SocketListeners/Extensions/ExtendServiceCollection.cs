using Warehouse.Server.SocketListeners;

namespace Microsoft.Extensions.DependencyInjection;

public static class ExtendServiceCollection
{
    public static IServiceCollection WithSocketListeners(this IServiceCollection self)
    {
        self.AddSingleton<SocketListener>(new SocketListener("localhost", 4242));
        return self;
    }
}
