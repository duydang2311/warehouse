using Warehouse.Server.SocketListeners;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class ExtendServiceCollection
{
    public static IServiceCollection WithSocketListeners(
        this IServiceCollection self,
        string hostname,
        int port
    )
    {
        self.AddSingleton<SocketListener>(new SocketListener(hostname, port));
        return self;
    }
}
