using Warehouse.Server.Applications;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class ExtendServiceCollection
{
	public static IServiceCollection WithApplications(this IServiceCollection self)
	{
		self.AddSingleton<IApplication, Application>();
		return self;
	}
}
