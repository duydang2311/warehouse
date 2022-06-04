using System.Net;
using System.Net.Sockets;
using System.Text;
using Warehouse.Shared.BinaryHelpers;

namespace Warehouse.Client.SocketClients;

public class SocketClient : ISocketClient
{
    private Socket? socket;
    private readonly IBinaryHelper binaryHelper;

    public SocketClient(IBinaryHelper binaryHelper)
    {
        this.binaryHelper = binaryHelper;
    }

    public Socket? Connect(string hostname, int port)
    {
        if (socket is not null)
        {
            socket.Disconnect(true);
        }
        var host = Dns.GetHostEntry(hostname);
        var ipAddress = host.AddressList[0];
        var remoteEP = new IPEndPoint(ipAddress, port);
        try
        {
            if (socket is null)
            {
                socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            }
            socket.Connect(remoteEP);
            Console.WriteLine($"Socket connected to {socket.RemoteEndPoint}");
            socket.Send(Encoding.ASCII.GetBytes("OK"));
            socket.Send(Encoding.ASCII.GetBytes("OK2"));
            socket.Send(Encoding.ASCII.GetBytes("OK3"));
            socket.Send(Encoding.ASCII.GetBytes("OK4"));
            socket.Send(Encoding.ASCII.GetBytes("OK5"));
            socket.Send(Encoding.ASCII.GetBytes("OK6"));
            socket.Shutdown(SocketShutdown.Receive);
            socket.Close();
            return socket;
        }
        catch
        {
#if DEBUG
            throw;
#else
            return null;
#endif
        }
    }
}
