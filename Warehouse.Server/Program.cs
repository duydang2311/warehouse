using Microsoft.Extensions.DependencyInjection;
using Warehouse.Server.SocketListeners;
using Warehouse.Server.SocketHandlers;
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
            .WithSocketHandlers()
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

    public static void Listener_Accepted(ISocketHandler client)
    {
        client.BeginReceive();
        client.Disconnecting += Client_Disconnecting;
        client.Received += Client_Received;
        Console.WriteLine($"{client.RemoteEndPoint} connected");
    }

    public static void Client_Disconnecting(ISocketHandler client)
    {
        Console.WriteLine($"Disposing handler {client.RemoteEndPoint} due to disconnected");
        client.Dispose();
    }

    public static void Client_Received(ISocketHandler client, IPacketHeader packet)
    {
        Console.Write(
            $"Packet {packet.Identity} has {packet.Buffer.Length} bytes: {string.Join(' ', packet.Buffer)}"
        );
    }
}
