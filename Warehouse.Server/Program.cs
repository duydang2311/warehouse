using Microsoft.Extensions.DependencyInjection;
using Warehouse.Server.Applications;
using Warehouse.Server.Databases;
using Warehouse.Server.Commands;
using Warehouse.Shared.Packets;
using Warehouse.Shared.Packets.Serializers;
using Warehouse.Shared.TcpServers;

namespace Warehouse.Server;

public partial class Program
{
	public static ServiceProvider Provider { get; private set; } = null!;
	private static IApplication App { get; set; } = null!;
	public static IDatabase Database { get; private set; } = null!;
	public static IPacketFactory PacketFactory { get; private set; } = null!;
	public static IPacketSerializer PacketSerializer { get; private set; } = null!;

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
			.WithCommands()
			.WithTcpClients()
			.WithTcpServers()
			.AddSingleton(provider => provider.GetRequiredService<ITcpServerFactory>().GetService("127.0.0.1", 4242));
		Provider = services.BuildServiceProvider();
		services.AddSingleton<IServiceProvider, ServiceProvider>(p => Provider);

		App = Provider.GetRequiredService<IApplication>();
		Database = Provider.GetRequiredService<IDatabase>();
		PacketFactory = Provider.GetRequiredService<IPacketFactory>();
		PacketSerializer = Provider.GetRequiredService<IPacketSerializer>();
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
		App.HandlePacketAsync<IAuthenticationPacket>(HandleAuthenticationPacket);
		App.Start();
		while (true)
			await App.ReadCommand();
	}
}
