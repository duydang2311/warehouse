using Warehouse.Server.Commands;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class ExtendServiceCollection
{
	public static IServiceCollection WithCommands(this IServiceCollection self)
	{
		self.AddSingleton<ICommandFactory, CommandFactory>();
		return self;
	}
}
