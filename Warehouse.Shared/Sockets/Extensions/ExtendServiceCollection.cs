using System.Net.Sockets;
using Warehouse.Shared.Services;
using Warehouse.Shared.Sockets;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class ExtendServiceCollection
{
	public static IServiceCollection WithSockets(this IServiceCollection self)
	{
		self.AddSingleton<ISocketFactory, SocketFactory>();
		return self;
	}
}
