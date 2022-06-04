using System.Net;
using System.Net.Sockets;
using System.Text;
using Warehouse.Server.SocketListeners;

namespace Warehouse.Server;

public class Program
{
	public static void Main()
	{
		new SocketListener();
	}
}
