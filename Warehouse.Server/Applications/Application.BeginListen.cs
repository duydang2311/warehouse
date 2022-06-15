using Warehouse.Shared.Packets;
using Warehouse.Shared.TcpServers;
using Warehouse.Server.SocketListeners;
using Warehouse.Server.SocketHandlers;

namespace Warehouse.Server.Applications;

public sealed partial class Application : IApplication
{
	public void Start()
	{
		Server.Start();
		Server.SessionCreated += SessionCreated;
		Console.WriteLine($"Listening on {Server.Address}:{Server.Port}");
	}
	private void SessionCreated(TcpServer sender, TcpSession session)
	{
		foreach (var i in packetDict)
		{
			session.Received += Received;
		}
		Console.WriteLine($"{session.Id} connected");
	}
	private async void Received(TcpSession sender, IPacketHeader header)
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
				await ((Func<TcpSession, IAuthenticationPacket, Task>)packetDict[typeof(IAuthenticationPacket)])(sender, packet);
			}
		}
	}
}
