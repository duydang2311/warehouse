using Warehouse.Server.Commands;

namespace Warehouse.Server.Applications;

public interface IApplication
{
	IDictionary<string, ICommand> Commands { get; }
	void RegisterPacketIdentifier();
	bool TryAuthenticateDatabase();
	bool TryAuthenticateRole();
	bool TryAddCommand(ICommand command);
	Task BeginReadCommand();
	Task<bool> RegisterAccount(string username, string password);
}
