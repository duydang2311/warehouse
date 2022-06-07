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
        listener.Accepted += Listener_Accepted;
        Console.ReadKey();
    }

    public static void Listener_Accepted(ISocketClient client)
    {
        client.BeginReceive();
        client.Disconnecting += Client_Disconnecting;
        Console.WriteLine($"{client.RemoteEndPoint} connected");
    }

    public static void Client_Disconnecting(ISocketClient client)
    {
        Console.WriteLine($"{client.RemoteEndPoint} disconnected");
    }
}
