using Microsoft.Extensions.DependencyInjection;
using Warehouse.Server.Applications;
using Warehouse.Server.Commands;

namespace Warehouse.Server;

public partial class Program
{
	public static ServiceProvider Provider { get; private set; } = null!;

	public static void Main()
	{
		var services = new ServiceCollection();
		services
			.WithSocketListeners()
			.WithSocketHandlers()
			.WithSockets()
			.WithPackets()
			.WithDatabases()
			.WithApplications()
			.WithCommands();
		Provider = services.BuildServiceProvider();
		services.AddSingleton<IServiceProvider, ServiceProvider>(p => Provider);

		var app = Provider.GetRequiredService<IApplication>();
		app.RegisterPacketIdentifier();
		if (!app.TryAuthenticateDatabase())
		{
			Console.WriteLine("Database authentication failed. Exiting the program");
			return;
		}
		Console.WriteLine("Database authenticated successfully");
		if (!app.TryAuthenticateRole())
		{
			Console.WriteLine("Role authentication failed. Exiting the program");
			return;
		}
		Console.WriteLine("Role authenticated successfully");
		var commandFactory = Provider.GetRequiredService<ICommandFactory>();
		app.TryAddCommand(commandFactory.GetService("exit", "Exit the program", ExitCommand));
		app.TryAddCommand(commandFactory.GetService("register", "Register a staff account", RegisterCommand));
		app.BeginReadCommand();
		CreateListener();
	}
}
