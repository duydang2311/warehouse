using Warehouse.Shared.Packets;
using Warehouse.Server.SocketHandlers;
using Warehouse.Server.Commands;
using Warehouse.Server.SocketListeners;
using Warehouse.Server.Databases;

namespace Warehouse.Server.Applications;

public interface IApplication
{
	IRoleAuth RoleAuth { get; }
	IDictionary<string, ICommand> Commands { get; }
	ISocketListener SocketListener { get; }
	bool TryAuthenticateDatabase();
	bool TryAuthenticateRole();
	bool TryAddCommand(ICommand command);
	Task ReadCommand();
	Task<bool> RegisterAccount(string username, string password);
	void BeginListen(string hostname, int port);
	void HandlePacketAsync<T>(Func<ISocketHandler, T, Task> handler) where T : IPacket;
}
