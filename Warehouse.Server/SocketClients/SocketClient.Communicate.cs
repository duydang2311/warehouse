using System.Net.Sockets;

namespace Warehouse.Server.SocketClients;

public partial class SocketClient : IDisposable
{
    private Thread receiveThread = null!;
    private Thread sendThread = null!;

    private void InitCommunicateInternal()
    {
        receiveThread = new Thread(ReceiveThread) { IsBackground = true };
        sendThread = new Thread(SendThread) { IsBackground = true };
        receiveThread.Start();
        sendThread.Start();
    }

    private void ReceiveThread()
    {
        while (true) { }
    }

    private void SendThread()
    {
        while (true) { }
    }
}
