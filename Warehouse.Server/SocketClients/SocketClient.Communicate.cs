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
        Console.WriteLine(client.ReceiveBufferSize + " " + client.SendBufferSize);
        if (read > 0)
        {
            receiveOffset += read;
        }
        else if (receiveOffset != 0)
        {
            Console.WriteLine($"Read {Encoding.ASCII.GetString(receiveBuffer)}");
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
