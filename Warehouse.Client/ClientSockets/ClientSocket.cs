using Warehouse.Shared.Sockets;
using Warehouse.Shared.Packets.Serializers;

namespace Warehouse.Client.ClientSockets;

public partial class ClientSocket : Socket, IClientSocket
{
	private readonly IPacketSerializer serializer;

	public ClientSocket(IPacketSerializer serializer, System.Net.Sockets.Socket socket)
		: base(socket)
	{
		this.serializer = serializer;
	}
}
