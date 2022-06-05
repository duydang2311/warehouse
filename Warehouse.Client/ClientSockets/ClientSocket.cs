using System.Net;
using System.Net.Sockets;
using Warehouse.Shared.Services;

namespace Warehouse.Client.ClientSockets;

using Warehouse.Shared.Sockets;

public class ClientSocket : IClientSocket
{
    private ISocket? socket;
    private readonly IServiceFactory<
        AddressFamily,
        SocketType,
        ProtocolType,
        ISocket
    > socketFactory;

    public ClientSocket(
        IServiceFactory<AddressFamily, SocketType, ProtocolType, ISocket> socketFactory
    )
    {
        this.socketFactory = socketFactory;
    }

    public async Task<bool> Connect(string hostname, int port)
    {
        if (socket is not null)
        {
            await socket.Disconnect(true);
        }
        var host = Dns.GetHostEntry(hostname);
        var ipAddress = host.AddressList[0];
        var remoteEP = new IPEndPoint(ipAddress, port);
        if (socket is null)
        {
            socket = socketFactory.GetService(
                ipAddress.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp
            );
        }
        return await socket.Connect(remoteEP);
    }
}
