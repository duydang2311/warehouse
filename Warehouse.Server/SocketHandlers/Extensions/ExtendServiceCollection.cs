using Warehouse.Server.SocketHandlers;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class ExtendServiceCollection
{
    public static IServiceCollection WithSocketHandlers(this IServiceCollection self)
    {
        self.AddSingleton<ISocketHandlerFactory, SocketHandlerFactory>();
        return self;
    }
}
