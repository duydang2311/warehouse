using Warehouse.Server.Databases;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class ExtendServiceCollection
{
	public static IServiceCollection WithDatabases(this IServiceCollection self)
	{
		self
			.AddSingleton<IDatabase, Database>()
			.AddSingleton<IRoleAuthFactory, RoleAuthFactory>();
		return self;
	}
}
