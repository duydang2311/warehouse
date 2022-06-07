using Microsoft.Extensions.DependencyInjection;
using Warehouse.Client.ClientSockets;
using Warehouse.Shared.Packets;
using Warehouse.Shared.Packets.Serializers;

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
        if (!await client.Connect("localhost", 4242))
        {
            Console.WriteLine("Connection failed");
            return;
        }
        Console.WriteLine("Connected successfully");
        Console.ReadKey();
    }
}
