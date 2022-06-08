using Microsoft.Extensions.DependencyInjection;
using Warehouse.Client.ClientSockets;
using Warehouse.Shared.NetHelpers;
using Warehouse.Shared.Packets;

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
        client.Received += Received;
        client.RemoteDisconnecting += RemoteDisconnecting;
        client.BeginReceive();
        Console.ReadKey();
    }

    private static void Received(IClientSocket sender, IPacketHeader packet)
    {
        Console.Write(
            $"Packet {packet.Identity} has {packet.Buffer.Length} bytes: {string.Join(' ', packet.Buffer)}"
        );
    }

    private static void RemoteDisconnecting(IClientSocket sender)
    {
        sender.Disconnect(false);
        sender.Dispose();
        Console.Write($"Server has disconnected. Current socket is disposed.");
    }
}
