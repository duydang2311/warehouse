using Warehouse.Shared.Packets;
using Warehouse.Server.SocketHandlers;

namespace Warehouse.Server.Applications;

public sealed partial class Application : IApplication
{
	private readonly Dictionary<Type, object> packetDict = new();
	public void HandlePacketAsync<T>(Func<ISocketHandler, T, Task> handler) where T : IPacket
	{
		packetDict.Add(typeof(T), handler);
	}
}
