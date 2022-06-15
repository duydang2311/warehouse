using Warehouse.Server.Manager.Databases;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class ExtendServiceCollection
{
	public static IServiceCollection WithDatabases(this IServiceCollection self)
	{
		self
			.AddSingleton<IDatabase, Database>()
			.AddSingleton<IRoleAuth, RoleAuth>(p => new RoleAuth { Name = "gearstore_staff", Password = "Staff12345" });
		return self;
	}
}
