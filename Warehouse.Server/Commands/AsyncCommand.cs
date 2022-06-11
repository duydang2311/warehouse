namespace Warehouse.Server.Commands;

public class AsyncCommand : IAsyncCommand
{
	private readonly Func<ICommand, string, Task> handler;
	public string Name { get; set; }
	public string Description { get; set; }
	public AsyncCommand(string name, string description, Func<ICommand, string, Task> handler)
	{
		Name = name.ToLower();
		Description = description;
		this.handler = handler;
	}
	public Task Execute(string input)
	{
		return handler(this, input);
	}
	void ICommand.Execute(string input)
	{
		_ = Execute(input);
	}
}
