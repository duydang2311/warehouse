using System.Net.Sockets;
using Warehouse.Shared.Services;

namespace Warehouse.Shared.Sockets;

public interface ISocketFactory
    : IServiceFactory<AddressFamily, SocketType, ProtocolType, ISocket> { }
