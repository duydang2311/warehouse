using Microsoft.Extensions.DependencyInjection;
using Warehouse.Server.SocketListeners;
using Warehouse.Server.SocketClients;

namespace Warehouse.Server;

public class Program
{
    public static ServiceProvider Provider { get; private set; } = null!;

    public static void Main()
    {
        var services = new ServiceCollection();
        services.WithBinaryHelpers().WithSocketListeners().WithSocketClients();
        Provider = services.BuildServiceProvider();
        services.AddSingleton<IServiceProvider, ServiceProvider>(p => Provider);

        var listener = Provider.GetRequiredService<ISocketListenerFactory>().GetService();
        listener.Bind("localhost", 4242);
        listener.BeginAccept();
        Console.ReadKey();
    }
}
