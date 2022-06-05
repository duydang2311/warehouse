using System.Net.Sockets;
using Warehouse.Shared.Services;
using Warehouse.Shared.Sockets;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class ExtendServiceCollection
{
    public static IServiceCollection WithSockets(this IServiceCollection self)
    {
        self.AddSingleton<
            IServiceFactory<AddressFamily, SocketType, ProtocolType, ISocket>,
            SocketFactory
        >();
        return self;
    }
}
