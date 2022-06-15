using System.Net.Sockets;
using Warehouse.Shared.Packets;
using Warehouse.Shared.Packets.Serializers;

namespace Warehouse.Shared.TcpClients;

public class TcpClient : NetCoreServer.TcpClient
{
	private readonly IPacketSerializer packetSerializer;
	public event Action<TcpClient, IPacketHeader>? Received;
	public TcpClient(IPacketSerializer packetSerializer, string address, int port) : base(address, port)
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

	public void DisconnectAndStop()
	{
		_stop = true;
		DisconnectAsync();
		while (IsConnected)
			Thread.Yield();
	}

	protected override void OnConnected()
	{
		Console.WriteLine($"Chat TCP client connected a new session with Id {Id}");
	}

	protected override void OnDisconnected()
	{
		Console.WriteLine($"Chat TCP client disconnected a session with Id {Id}");

		// Wait for a while...
		Thread.Sleep(1000);

		// Try to connect again
		if (!_stop)
			ConnectAsync();
	}

	protected override async void OnReceived(byte[] buffer, long offset, long size)
	{
		using var stream = new MemoryStream(buffer);
		var header = await packetSerializer.TryDeserializeAsync(stream);
		if (header is not null)
		{
			System.Diagnostics.Debug.WriteLine("received?.invoke");
			Received?.Invoke(this, header);
		}
		System.Diagnostics.Debug.WriteLine(header is null);
		System.Diagnostics.Debug.WriteLine(string.Join(',', buffer, offset, size));
	}

	protected override void OnError(SocketError error)
	{
		Console.WriteLine($"Chat TCP client caught an error with code {error}");
	}

	private bool _stop;
}

