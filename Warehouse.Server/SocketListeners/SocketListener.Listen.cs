using System.Text;
using Warehouse.Server.SocketClients;

namespace Warehouse.Server.SocketListeners;

public sealed partial class SocketListener
{
    private bool listening = false;
    private Thread listenThread = null!;

    public void Listen()
    {
        if (listening)
        {
            if (listenThread is not null)
            {
                return;
            }
        }
        Console.WriteLine("Listen...");
        listening = true;
        listenThread = new Thread(ListenThread) { IsBackground = true };
        listenThread.Start();
    }

    private void ListenThread()
    {
        for (; ; )
        {
            Console.WriteLine("Waiting for client...");
            while (!listening)
                ;
            listener.Listen(0);
            var client = listener.Accept();
            Clients.Add(socketClientFactory.GetService(client));
            Console.WriteLine($"{client.RemoteEndPoint} connected");
        }
    }
}
