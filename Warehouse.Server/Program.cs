using Microsoft.Extensions.DependencyInjection;
using Warehouse.Server.SocketListeners;

namespace Warehouse.Server;

public class Program
{
    public static ServiceProvider Provider { get; private set; } = null!;

    public static void Main()
    {
        var services = new ServiceCollection();
        services.WithBinaryHelpers().WithSocketListeners("localhost", 4242);
        Provider = services.BuildServiceProvider();

        var listener = Provider.GetRequiredService<SocketListener>();
        listener.Listen();
        Console.ReadKey();
    }
}
