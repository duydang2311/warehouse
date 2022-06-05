using System.Net;
using System.Net.Sockets;
using Warehouse.Shared.Services;

namespace Warehouse.Client.SocketClients;

using Warehouse.Shared.Sockets;

public class SocketClient : ISocketClient
{
    private ISocket? socket;
    private readonly IServiceFactory<
        AddressFamily,
        SocketType,
        ProtocolType,
        ISocket
    > socketFactory;

    public SocketClient(
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
