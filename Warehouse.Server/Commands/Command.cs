namespace Warehouse.Server.Commands;

public class Command : ICommand
{
	private readonly Action<ICommand, string> handler;
	public string Name { get; set; }
	public string Description { get; set; }
	public Command(string name, string description, Action<ICommand, string> handler)
	{
		Name = name.ToLower();
		Description = description;
		this.handler = handler;
	}
	public void Execute(string input)
	{
		handler(this, input);
	}
}
