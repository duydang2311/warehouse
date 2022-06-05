using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Warehouse.Client.SocketClients;
using Warehouse.Shared.BinaryHelpers;

namespace Warehouse.Client;

public class Program
{
    public static ServiceProvider Provider { get; protected set; } = null!;

    public static async Task Main()
    {
        var services = new ServiceCollection();
        services.WithBinaryHelpers().WithSockets().WithSocketClients();
        Provider = services.BuildServiceProvider();

        var client = Provider.GetRequiredService<ISocketClient>();
        if (!await client.Connect("localhost", 4242))
        {
            Console.WriteLine("Connection failed");
            return;
        }
        Console.WriteLine("Connected successfully");
        Console.ReadKey();
    }
}
