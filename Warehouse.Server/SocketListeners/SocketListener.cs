using System.Net;
using System.Net.Sockets;
using Warehouse.Server.SocketClients;

namespace Warehouse.Server.SocketListeners;

public sealed partial class SocketListener
{
    public List<SocketClient> Clients { get; }
    private readonly Socket listener;

    public SocketListener(string hostname, int port)
    {
        Clients = new List<SocketClient>();
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
