namespace Warehouse.Server.Commands;

public interface IAsyncCommand : ICommand
{
	new Task Execute(string input);
}
