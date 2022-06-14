using Warehouse.Shared.Sockets.Clients;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class ExtendServiceCollection
{
	public static IServiceCollection WithSocketClients(this IServiceCollection self)
	{
		self.AddSingleton<IClientSocketFactory, ClientSocketFactory>();
		return self;
	}
}
