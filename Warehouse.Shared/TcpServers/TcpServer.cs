using System.Net.Sockets;
using Warehouse.Shared.Packets;
using Warehouse.Shared.Packets.Serializers;

namespace Warehouse.Shared.TcpServers;

public class TcpSession : NetCoreServer.TcpSession
{
	private readonly IPacketSerializer packetSerializer;
	public event Action<TcpSession, IPacketHeader>? Received;
	public TcpSession(IPacketSerializer packetSerializer, TcpServer server) : base(server)
	{
		this.packetSerializer = packetSerializer;
	}

	public async Task<bool> SendAsync(IPacketHeader header)
	{
		var stream = await packetSerializer.TrySerializeAsync(header) as MemoryStream;
		return SendAsync(stream!.ToArray());
	}

	public long Send(IPacketHeader header)
	{
		return Send(packetSerializer.TrySerialize(header)!);
	}
	protected override void OnConnected()
	{
		Console.WriteLine($"TCP session with Id {Id} connected!");
	}

	protected override void OnDisconnected()
	{
		Console.WriteLine($"TCP session with Id {Id} disconnected!");
	}

	protected override async void OnReceived(byte[] buffer, long offset, long size)
	{
		using var stream = new MemoryStream(buffer);
		var header = await packetSerializer.TryDeserializeAsync(stream);
		if (header is not null)
		{
			Received?.Invoke(this, header);
		}
		Console.WriteLine(header is null);
		Console.WriteLine(string.Join(',', buffer, offset, size));
	}

	protected override void OnError(SocketError error)
	{
		Console.WriteLine($"Chat TCP session caught an error with code {error}");
	}
}

public class TcpServer : NetCoreServer.TcpServer
{
	private readonly IPacketSerializer packetSerializer;
	public event Action<TcpServer, TcpSession>? SessionCreated;
	public TcpServer(IPacketSerializer packetSerializer, string address, int port) : base(address, port)
	{
		this.packetSerializer = packetSerializer;
	}

	protected override NetCoreServer.TcpSession CreateSession()
	{
		var session = new TcpSession(packetSerializer, this);
		SessionCreated?.Invoke(this, session);
		return session;
	}

	protected override void OnError(SocketError error)
	{
		Console.WriteLine($"Chat TCP server caught an error with code {error}");
	}
}
