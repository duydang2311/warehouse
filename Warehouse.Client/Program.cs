using Microsoft.Extensions.DependencyInjection;
using Warehouse.Client.ClientSockets;
using Warehouse.Shared.NetHelpers;

namespace Warehouse.Client;

public class Program
{
    public static ServiceProvider Provider { get; protected set; } = null!;

    public static async Task Main()
    {
        var services = new ServiceCollection();
        services.WithBinaryHelpers().WithSockets().WithSocketClients().WithPackets();
        Provider = services.BuildServiceProvider();

        var factory = Provider.GetRequiredService<IClientSocketFactory>();
        var client = factory.GetService();
        var endpoint = NetHelper.MakeEndPointWith("localhost", 4242);
        if (!await client.Connect(endpoint))
        {
            Console.WriteLine($"Could not connect to {endpoint}");
            return;
        }
        Console.WriteLine($"Connected to {client.RemoteEndPoint}");
        Console.ReadKey();
    }
}
