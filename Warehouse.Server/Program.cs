using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Warehouse.Server.SocketListeners;
using Warehouse.Server.SocketHandlers;
using Warehouse.Shared.Packets;
using Warehouse.Shared.Packets.Identifiers;

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

        var listener = Provider.GetRequiredService<ISocketListenerFactory>().GetService();
        listener.Bind("localhost", 4242);
        listener.BeginAccept();
        listener.Accepted += Listener_Accepted;
        Console.ReadKey();
    }

    private static void Listener_Accepted(ISocketListener sender, ISocketHandler handler)
    {
        handler.BeginReceive();
        handler.Disconnecting += Client_Disconnecting;
        handler.Received += Client_Received;
        Console.WriteLine($"{handler.RemoteEndPoint} connected");
    }

    private static void Client_Disconnecting(ISocketHandler client)
    {
        Console.WriteLine($"Disposing handler {client.RemoteEndPoint} due to disconnected");
        client.Dispose();
    }

    private static void Client_Received(ISocketHandler client, IPacketHeader packet)
    {
        Console.Write(
            $"Packet {packet.Identity} has {packet.Buffer.Length} bytes: {string.Join(' ', packet.Buffer)}"
        );
    }
}
