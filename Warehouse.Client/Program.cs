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

    public static void Main()
    {
        var services = new ServiceCollection();
        services.WithBinaryHelpers().WithSocketClients();
        Provider = services.BuildServiceProvider();

        var client = Provider.GetRequiredService<ISocketClient>();
        client.Connect("localhost", 4242);
        for (; ; )
            ;
    }
}
