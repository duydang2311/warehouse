using System.Net;
using System.Net.Sockets;

namespace Warehouse.Shared.Sockets;

public partial class Socket : ISocket
{
	public void Bind(EndPoint localEP) => socket.Bind(localEP);

	public void Bind(string hostname, int port)
	{
		var ipAddress = Dns.GetHostEntry(hostname, AddressFamily.InterNetworkV6).AddressList[0];
		Bind(new IPEndPoint(ipAddress, port));
	}
}
