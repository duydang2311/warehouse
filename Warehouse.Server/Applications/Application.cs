using Warehouse.Shared.Packets.Identifiers;
using Warehouse.Server.Databases;
using Warehouse.Server.Commands;

namespace Warehouse.Server.Applications;

public sealed partial class Application : IApplication
{
	private readonly IDatabase database;
	private readonly IRoleAuth roleAuth;
	private readonly IPacketIdentifier packetIdentifier;
	private readonly ICommandFactory commandFactory;
	public Application(IDatabase database, IRoleAuth roleAuth, IPacketIdentifier packetIdentifier, ICommandFactory commandFactory)
	{
		this.database = database;
		this.roleAuth = roleAuth;
		this.packetIdentifier = packetIdentifier;
		this.commandFactory = commandFactory;
	}
}
