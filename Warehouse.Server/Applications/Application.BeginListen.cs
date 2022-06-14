using Warehouse.Shared.Packets;
using Warehouse.Server.Databases;
using Warehouse.Server.Commands;
using Warehouse.Server.SocketListeners;
using Warehouse.Server.SocketHandlers;

namespace Warehouse.Server.Applications;

public sealed partial class Application : IApplication
{
	public void BeginListen(string hostname, int port)
	{
		SocketListener.Bind(hostname, port);
		SocketListener.BeginAccept();
		SocketListener.Accepted += Listener_Accepted;
		Console.WriteLine($"Listening on {SocketListener.LocalEndPoint}");
	}
	private static void Listener_Accepted(ISocketListener sender, ISocketHandler handler)
	{
		handler.BeginReceive();
		handler.Disconnecting += Client_Disconnecting;
		handler.Received += Client_Received;
		Console.WriteLine($"{handler.RemoteEndPoint} connected");
	}

	private static void Client_Disconnecting(ISocketHandler client)
	{
		Console.WriteLine($"Disposing handler {client.RemoteEndPoint} due to disconnected");
		client.Dispose();
	}

	private static void Client_Received(ISocketHandler client, IPacketHeader packet)
	{
		Console.WriteLine(
			$"Packet {packet.Identity} has {packet.Buffer.Length} bytes: {string.Join(' ', packet.Buffer)}"
		);
	}
}
