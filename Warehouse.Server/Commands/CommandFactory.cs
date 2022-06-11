namespace Warehouse.Server.Commands;

public class CommandFactory : ICommandFactory
{
	public ICommand GetService(string name, string description, Action<ICommand, string> handler)
		=> new Command(name, description, handler);
}
