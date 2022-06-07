using Warehouse.Shared.BinaryHelpers;
using Warehouse.Shared.Packets;

namespace Warehouse.Server.SocketClients;

public partial class SocketClient : IDisposable
{
    private const int BufferSize = 8192;
    private byte[] receiveBuffer = null!;

    public void BeginReceive()
    {
        receiveBuffer = new byte[BufferSize];
        BeginReceive(receiveBuffer, ReceiveCallback);
    }

    private void ReceiveCallback(IAsyncResult asyncResult)
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
        int bytesRead = 0;
        var buffer = new ReadOnlyMemory<byte>(receiveBuffer, 0, bytes);
        do
        {
            try
            {
                var packet = new BinaryHelper().Deserialize<IPacket>(buffer, out bytesRead);
                Console.Write($"Packet {packet.Identity} ({bytesRead} bytes read): ");
                foreach (var i in packet.Buffer)
                {
                    Console.Write(i + " ");
                }
                buffer = buffer.Slice(bytesRead);
                Console.WriteLine(buffer.Length);
            }
#pragma warning disable 0168
            catch (Exception ex)
#pragma warning restore 0168
            {
#if DEBUG
                throw;
#else
                Console.WriteLine(ex);
                break;
#endif
            }
        } while (buffer.Length != 0);
        BeginReceive(receiveBuffer, ReceiveCallback);
    }
}
