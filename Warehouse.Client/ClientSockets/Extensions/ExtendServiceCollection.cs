using Warehouse.Client.ClientSockets;

namespace Microsoft.Extensions.DependencyInjection;

public static class ExtendServiceCollection
{
    public static IServiceCollection WithSocketClients(this IServiceCollection self)
    {
        self.AddTransient<IClientSocket, ClientSocket>();
        return self;
    }
}
