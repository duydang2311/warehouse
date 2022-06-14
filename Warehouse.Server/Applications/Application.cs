using Warehouse.Shared.Packets.Identifiers;
using Warehouse.Shared.Packets.Serializers;
using Warehouse.Server.Databases;
using Warehouse.Server.Commands;
using Warehouse.Server.SocketListeners;

namespace Warehouse.Server.Applications;

public sealed partial class Application : IApplication
{
	private readonly IDatabase database;
	private readonly IPacketIdentifier packetIdentifier;
	private readonly IPacketSerializer packetSerializer;
	public ISocketListener SocketListener { get; }
	public IRoleAuth RoleAuth { get; }
	public Application(IDatabase database, IRoleAuth roleAuth, IPacketIdentifier packetIdentifier, IPacketSerializer packetSerializer, ISocketListenerFactory socketListenerFactory)
	{
		this.database = database;
		this.RoleAuth = roleAuth;
		this.packetIdentifier = packetIdentifier;
		this.packetSerializer = packetSerializer;
		SocketListener = socketListenerFactory.GetService();
	}
}
