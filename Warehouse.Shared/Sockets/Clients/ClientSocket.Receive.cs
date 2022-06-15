using Warehouse.Shared.Packets;

namespace Warehouse.Shared.Sockets.Clients;

public partial class ClientSocket
{
	public event Action<IClientSocket, IPacketHeader>? Received;
	private const int BufferSize = 8192;
	private byte[] receiveBuffer = null!;

	public void BeginReceive()
	{
		receiveBuffer = new byte[BufferSize];
		BeginReceive(receiveBuffer, ReceiveCallback);
	}

	private async void ReceiveCallback(IAsyncResult asyncResult)
	{
		if (!Connected)
		{
			RemoteDisconnect();
			return;
		}
		var bytes = EndReceive(asyncResult);
		if (bytes == 0)
		{
			RemoteDisconnect();
			return;
		}
		long offset = 0;
		long position = 0;
		do
		{
			using var stream = new MemoryStream(receiveBuffer, (int)offset, bytes - (int)offset);
			var packet = await serializer.TryDeserializeAsync(stream);
			if (packet is null)
			{
				Console.WriteLine("Bad packet");
				break;
			}
			System.Diagnostics.Debug.WriteLine("Receive " + packet.Identity);
			if (Received is not null)
			{
				Received(this, packet);
			}
			offset += position;
			position = stream.Position;
		} while (offset + position < bytes);
		System.Diagnostics.Debug.WriteLine("Done?");
		BeginReceive(receiveBuffer, ReceiveCallback);
	}
}
