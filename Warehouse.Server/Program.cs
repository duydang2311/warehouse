using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Warehouse.Server.SocketListeners;
using Warehouse.Server.SocketHandlers;
using Warehouse.Shared.Packets;
using Warehouse.Shared.Packets.Identifiers;

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
			.WithDatabases();
		Provider = services.BuildServiceProvider();
		services.AddSingleton<IServiceProvider, ServiceProvider>(p => Provider);
		Authenticate();

		var identifier = Provider.GetRequiredService<IPacketIdentifier>();
		identifier.Register(Assembly.GetExecutingAssembly());
		foreach (var i in Assembly.GetExecutingAssembly().GetReferencedAssemblies())
		{
			if (i.Name == "Warehouse.Shared")
			{
				identifier.Register(Assembly.Load(i));
				break;
			}
		}
		CreateListener();
		ReadCommand();
	}
}
