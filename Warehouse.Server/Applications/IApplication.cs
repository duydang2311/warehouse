using Warehouse.Server.Commands;

namespace Warehouse.Server.Applications;

public interface IApplication
{
	void RegisterPacketIdentifier();
	bool TryAuthenticateDatabase();
	bool TryAuthenticateRole();
	bool TryAddCommand(ICommand command);
	void BeginReadCommand();
}