using Warehouse.Server.Commands;
using Warehouse.Server.SocketListeners;

namespace Warehouse.Server.Applications;

public interface IApplication
{
	IDictionary<string, ICommand> Commands { get; }
	ISocketListener SocketListener { get; }
	void RegisterPacketIdentifier();
	bool TryAuthenticateDatabase();
	bool TryAuthenticateRole();
	bool TryAddCommand(ICommand command);
	Task ReadCommand();
	Task<bool> RegisterAccount(string username, string password);
	void BeginListen(string hostname, int port);
}
