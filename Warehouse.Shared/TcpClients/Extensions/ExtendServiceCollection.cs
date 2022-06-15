using Warehouse.Shared.TcpClients;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class ExtendServiceCollection
{
	public static IServiceCollection WithTcpClients(this IServiceCollection self)
	{
		self.AddSingleton<ITcpClientFactory, TcpClientFactory>();
		return self;
	}
}
