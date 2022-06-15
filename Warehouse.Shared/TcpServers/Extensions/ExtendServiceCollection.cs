using Warehouse.Shared.TcpServers;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class ExtendServiceCollection
{
	public static IServiceCollection WithTcpServers(this IServiceCollection self)
	{
		self.AddSingleton<ITcpServerFactory, TcpServerFactory>();
		return self;
	}
}
