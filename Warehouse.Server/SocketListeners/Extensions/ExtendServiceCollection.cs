using Warehouse.Server.SocketListeners;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class ExtendServiceCollection
{
    public static IServiceCollection WithSocketListeners(this IServiceCollection self)
    {
        self.AddSingleton<ISocketListenerFactory, SocketListenerFactory>();
        return self;
    }
}
