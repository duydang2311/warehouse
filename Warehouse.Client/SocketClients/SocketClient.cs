using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Warehouse.Client.SocketClients;

public class SocketClient
{
	public SocketClient()
	{
		var host = Dns.GetHostEntry("localhost");
		var ipAddress = host.AddressList[0];
		var remoteEP = new IPEndPoint(ipAddress, 11000);
		var sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
		try
		{
			sender.Connect(remoteEP);
			Console.WriteLine($"Socket connected to {sender.RemoteEndPoint}");
			var bytes = Encoding.ASCII.GetBytes("Hello world.");
			var bytesSent = sender.Send(bytes);
			int bytesReceived = sender.Receive(bytes);
			Console.WriteLine($"Receive {Encoding.ASCII.GetString(bytes, 0, bytesReceived)}");
			sender.Shutdown(SocketShutdown.Both);
			sender.Close();
			Console.WriteLine($"Receive {Encoding.ASCII.GetString(bytes, 0, bytesReceived)}");
			sender.Shutdown(SocketShutdown.Both);
		}
		catch
		{
			throw;
		}
	}
}
