using Microsoft.Extensions.DependencyInjection;
using Warehouse.Server.SocketListeners;
using Warehouse.Server.SocketClients;
using Warehouse.Shared.Packets;

namespace Warehouse.Server;

public class Program
{
    public static ServiceProvider Provider { get; private set; } = null!;

    public static void Main()
    {
        var services = new ServiceCollection();
        services
            .WithBinaryHelpers()
            .WithSocketListeners()
            .WithSocketClients()
            .WithSockets()
            .WithPackets();
        Provider = services.BuildServiceProvider();
        services.AddSingleton<IServiceProvider, ServiceProvider>(p => Provider);

        var listener = Provider.GetRequiredService<ISocketListenerFactory>().GetService();
        listener.Bind("localhost", 4242);
        listener.BeginAccept();
        listener.Accepted += Listener_Accepted;
        Console.ReadKey();
    }

    public static void Listener_Accepted(ISocketClient client)
    {
        client.BeginReceive();
        client.Disconnecting += Client_Disconnecting;
        client.Received += Client_Received;
        Console.WriteLine($"{client.RemoteEndPoint} connected");
    }

    public static void Client_Disconnecting(ISocketClient client)
    {
        Console.WriteLine($"{client.RemoteEndPoint} disconnected. Disposing its handler");
        client.Dispose();
    }

    public static void Client_Received(ISocketClient client, IPacketHeader packet)
    {
        Console.Write(
            $"Packet {packet.Identity} has {packet.Buffer.Length} bytes: {string.Join(' ', packet.Buffer)}"
        );
    }
}
