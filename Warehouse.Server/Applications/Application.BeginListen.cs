using System;
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
	private void Listener_Accepted(ISocketListener sender, ISocketHandler handler)
	{
		handler.BeginReceive();
		handler.Disconnecting += Client_Disconnecting;
		handler.Received += Client_Received;
		Console.WriteLine($"{handler.RemoteEndPoint} connected");
	}

	private void Client_Disconnecting(ISocketHandler client)
	{
		Console.WriteLine($"Disposing handler {client.RemoteEndPoint} due to disconnected");
		client.Dispose();
	}

	private async void Client_Received(ISocketHandler handler, IPacketHeader header)
	{
		if (packetIdentifier.Is<IAuthenticationPacket>(header))
		{
			if (!packetDict.ContainsKey(typeof(IAuthenticationPacket)))
			{
				return;
			}
			var packet = await packetSerializer.TryDeserializeAsync<IAuthenticationPacket>(header);
			if (packet is not null)
			{
				await ((Func<ISocketHandler, IAuthenticationPacket, Task>)packetDict[typeof(IAuthenticationPacket)])(handler, packet);
			}
		}
	}
}
