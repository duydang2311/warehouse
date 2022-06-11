using Microsoft.Extensions.DependencyInjection;
using Warehouse.Server.Applications;
using Warehouse.Server.Commands;

namespace Warehouse.Server;

public partial class Program
{
	public static ServiceProvider Provider { get; private set; } = null!;
	private static IApplication App { get; set; } = null!;

	public static async Task Main()
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

		App = Provider.GetRequiredService<IApplication>();
		App.RegisterPacketIdentifier();
		if (!App.TryAuthenticateDatabase())
		{
			Console.WriteLine("Database authentication failed. Exiting the program");
			return;
		}
		Console.WriteLine("Database authenticated successfully");
		if (!App.TryAuthenticateRole())
		{
			Console.WriteLine("Role authentication failed. Exiting the program");
			return;
		}
		Console.WriteLine("Role authenticated successfully");
		var commandFactory = Provider.GetRequiredService<ICommandFactory>();
		App.TryAddCommand(commandFactory.GetService("help", "List all available commands", HelpCommand));
		App.TryAddCommand(commandFactory.GetService("exit", "Exit the program", ExitCommand));
		App.TryAddCommand(commandFactory.GetService("quit", "Exit the program (same with 'exit')", ExitCommand));
		App.TryAddCommand(commandFactory.GetAsyncService("register", "Register a staff account", RegisterCommand));
		await App.BeginReadCommand();
		CreateListener();
	}
}
