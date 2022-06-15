using Warehouse.Shared.Packets;
using Warehouse.Server.SocketHandlers;
using Warehouse.Server.Commands;
using Warehouse.Server.Databases;
using Warehouse.Shared.TcpServers;

namespace Warehouse.Server.Applications;

public interface IApplication
{
	IRoleAuth RoleAuth { get; }
	IDictionary<string, ICommand> Commands { get; }
	TcpServer Server { get; }
	bool TryAuthenticateDatabase();
	bool TryAuthenticateRole();
	bool TryAddCommand(ICommand command);
	Task ReadCommand();
	Task<bool> RegisterAccount(string username, string password);
	void HandlePacketAsync<T>(Func<TcpSession, T, Task> handler) where T : IPacket;
	void Start();
}
