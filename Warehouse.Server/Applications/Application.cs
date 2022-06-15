using Warehouse.Shared.Packets.Identifiers;
using Warehouse.Shared.Packets.Serializers;
using Warehouse.Shared.TcpServers;
using Warehouse.Server.Databases;
using Warehouse.Server.Commands;
using Warehouse.Server.SocketListeners;

namespace Warehouse.Server.Applications;

public sealed partial class Application : IApplication
{
	private readonly IDatabase database;
	private readonly IPacketIdentifier packetIdentifier;
	private readonly IPacketSerializer packetSerializer;
	public TcpServer Server { get; }
	public IRoleAuth RoleAuth { get; }
	public Application(IDatabase database, IRoleAuth roleAuth, IPacketIdentifier packetIdentifier, IPacketSerializer packetSerializer, TcpServer tcpServer)
	{
		this.database = database;
		this.RoleAuth = roleAuth;
		this.packetIdentifier = packetIdentifier;
		this.packetSerializer = packetSerializer;
		Server = tcpServer;
	}
}
