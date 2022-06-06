namespace Warehouse.Client.ClientSockets;

using Warehouse.Shared.Sockets;

public partial class ClientSocket : IClientSocket
{
    public ISocket? Socket { get; protected set; }
    private readonly ISocketFactory socketFactory;

    public ClientSocket(ISocketFactory socketFactory)
    {
        this.socketFactory = socketFactory;
    }
}
