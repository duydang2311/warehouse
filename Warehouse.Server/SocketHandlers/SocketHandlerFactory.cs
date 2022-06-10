using System.Net.Sockets;
using Warehouse.Shared.Packets.Serializers;

namespace Warehouse.Server.SocketHandlers;

public class SocketHandlerFactory : ISocketHandlerFactory
{
	private readonly IPacketSerializer serializer;

	public SocketHandlerFactory(IPacketSerializer serializer)
	{
		this.serializer = serializer;
	}

	public ISocketHandler GetService(System.Net.Sockets.Socket socket) =>
		new SocketHandler(serializer, socket);

	public ISocketHandler GetService() =>
		new SocketHandler(
			serializer,
			new System.Net.Sockets.Socket(
				AddressFamily.InterNetworkV6,
				SocketType.Stream,
				ProtocolType.Tcp
			)
		);
}
