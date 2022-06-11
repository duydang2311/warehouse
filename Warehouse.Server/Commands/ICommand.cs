namespace Warehouse.Server.Commands;

public interface ICommand
{
	string Name { get; }
	string Description { get; set; }
	void Execute(string input);
}
