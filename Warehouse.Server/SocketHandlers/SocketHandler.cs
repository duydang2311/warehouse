using Warehouse.Shared.Sockets;
using Warehouse.Shared.Packets.Serializers;

namespace Warehouse.Server.SocketHandlers;

public partial class SocketHandler : Socket, ISocketHandler
{
	private readonly IPacketSerializer serializer;

	public SocketHandler(IPacketSerializer serializer, System.Net.Sockets.Socket socket)
		: base(socket)
	{
		this.serializer = serializer;
	}
}
