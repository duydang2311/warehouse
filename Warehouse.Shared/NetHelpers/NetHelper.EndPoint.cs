using System.Net;

namespace Warehouse.Shared.NetHelpers;

public static partial class NetHelper
{
    public static EndPoint MakeEndPointWith(string hostname, int port)
    {
        return new IPEndPoint(Dns.GetHostEntry(hostname).AddressList[0], port);
    }
}
