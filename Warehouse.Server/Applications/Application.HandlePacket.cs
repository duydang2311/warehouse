using Warehouse.Shared.Packets;
using Warehouse.Shared.TcpServers;

namespace Warehouse.Server.Applications;

public sealed partial class Application : IApplication
{
	private readonly Dictionary<Type, object> packetDict = new();
	public void HandlePacketAsync<T>(Func<TcpSession, T, Task> handler) where T : IPacket
	{
		packetDict.Add(typeof(T), handler);
	}
}
