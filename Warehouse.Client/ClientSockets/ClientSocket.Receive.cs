using Warehouse.Shared.BinaryHelpers;
using Warehouse.Shared.Packets;

namespace Warehouse.Client.ClientSockets;

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
        int offset = 0;
        long position = 0;
        do
        {
            using var stream = new MemoryStream(receiveBuffer, offset, bytes - offset);
            var packet = await serializer.TryDeserializeAsync(stream);
            if (packet is null)
            {
                Console.WriteLine("Bad packet");
                break;
            }
            if (Received is not null)
            {
                Received(this, packet);
            }
            position = stream.Position;
        } while (offset + position < bytes);
        BeginReceive(receiveBuffer, ReceiveCallback);
    }
}
