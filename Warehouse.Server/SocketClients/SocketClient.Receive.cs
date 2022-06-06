using System.Net.Sockets;
using System.Text;

namespace Warehouse.Server.SocketClients;

public partial class SocketClient : IDisposable
{
    private const int BufferSize = 8192;
    private int receiveOffset = 0;
    private byte[] receiveBuffer = null!;
    private byte[] sendBuffer = null!;

    private void InitCommunicateInternal()
    {
        receiveBuffer = new byte[BufferSize];
        sendBuffer = new byte[BufferSize];
        client.BeginReceive(
            receiveBuffer,
            receiveOffset,
            BufferSize - receiveOffset,
            SocketFlags.None,
            new AsyncCallback(ReceiveCallback),
            null
        );
    }

    private void ReceiveCallback(IAsyncResult result)
    {
        if (!client.Connected)
        {
            Disconnect();
            return;
        }
        var read = client.EndReceive(result);
        if (read == 0)
        {
            Disconnect();
            return;
        }
        if (read > 0)
        {
            receiveOffset += read;
        }
        else
        {
            // TODO: buffer is fulfilled, do something
            receiveOffset = 0;
            Array.Clear(receiveBuffer);
        }
        client.BeginReceive(
            receiveBuffer,
            receiveOffset,
            BufferSize - receiveOffset,
            SocketFlags.None,
            new AsyncCallback(ReceiveCallback),
            null
        );
    }
}
