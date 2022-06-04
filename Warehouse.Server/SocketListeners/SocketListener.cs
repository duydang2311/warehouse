using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

namespace Warehouse.Server.SocketListeners;

public class SocketListener
{
	public SocketListener()
	{
		var host = Dns.GetHostEntry("localhost");
		var ipAddress = host.AddressList[0];
		var localEP = new IPEndPoint(ipAddress, 11000);
		try
		{
			var listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			listener.Bind(localEP);
			listener.Listen(10);

			var handler = listener.Accept();
			Console.WriteLine(handler);
			string data = "";
			byte[] bytes;
			var stream = new NetworkStream(handler);
			stream.

			while (true)
			{
				bytes = new byte[1024];
				int bytesRec = handler.Receive(bytes);
				data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
				Console.WriteLine(data);
				if (data.IndexOf("<EOF>") > -1)
				{
					break;
				}
			}

			Console.WriteLine("Text received : {0}", data);
			byte[] msg = Encoding.ASCII.GetBytes(data);
			handler.Send(msg);
			handler.Shutdown(SocketShutdown.Both);
			handler.Close();
		}
		catch
		{
			throw;
		}
	}
}
