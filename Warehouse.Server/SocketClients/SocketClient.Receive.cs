using Warehouse.Shared.BinaryHelpers;
using Warehouse.Shared.Packets;

namespace Warehouse.Server.SocketClients;

public partial class SocketClient : IDisposable
{
    public event Action<ISocketClient, IPacket>? Received;
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
            Disconnect();
            return;
        }
        var bytes = EndReceive(asyncResult);
        if (bytes == 0)
        {
            Disconnect();
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
