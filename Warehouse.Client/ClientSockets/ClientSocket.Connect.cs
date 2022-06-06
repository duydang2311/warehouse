using System.Net;
using System.Net.Sockets;

namespace Warehouse.Client.ClientSockets;

public partial class ClientSocket : IClientSocket
{
    public async Task<bool> Connect(string hostname, int port)
    {
        var host = Dns.GetHostEntry(hostname);
        var ipAddress = host.AddressList[0];
        var remoteEP = new IPEndPoint(ipAddress, port);
        if (Socket is null)
        {
            Socket = socketFactory.GetService(
                ipAddress.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp
            );
        }
        else
        {
            await Socket.Disconnect(true);
        }
        return await Socket.Connect(remoteEP);
    }
}
