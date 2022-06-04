using System.Net;
using System.Net.Sockets;
using Warehouse.Server.SocketClients;
using Warehouse.Shared.Services;

namespace Warehouse.Server.SocketListeners;

public sealed partial class SocketListener
{
    public List<ISocketClient> Clients { get; }
    private readonly Socket listener;
    private readonly IServiceFactory<Socket, ISocketClient> socketClientFactory;

    public SocketListener(
        IServiceFactory<Socket, ISocketClient> socketClientFactory,
        string hostname,
        int port
    )
    {
        this.socketClientFactory = socketClientFactory;
        Clients = new List<ISocketClient>();
        var host = Dns.GetHostEntry(hostname);
        var ipAddress = host.AddressList[0];
        var localEP = new IPEndPoint(ipAddress, port);
        try
        {
            listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(localEP);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
#if DEBUG
            throw;
#endif
        }
    }
}
