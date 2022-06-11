using Warehouse.Server.Commands;

namespace Warehouse.Server.Applications;

public sealed partial class Application : IApplication
{
	public IDictionary<string, ICommand> Commands { get; } = new Dictionary<string, ICommand>();
	public bool TryAddCommand(ICommand command)
	{
		return Commands.TryAdd(command.Name, command);
	}
	public async Task BeginReadCommand()
	{
		while (true)
		{
			var line = Console.ReadLine()?.Trim();
			if (string.IsNullOrEmpty(line))
			{
				continue;
			}
			var args = line.Split(' ', 2);
			args[0] = args[0].ToLower();
			if (Commands.ContainsKey(args[0]))
			{
				var command = Commands[args[0]];
				if (command is IAsyncCommand asyncCommand)
				{
					await asyncCommand.Execute(args.Length == 1 ? "" : args[1]);
					continue;
				}
				command.Execute(args.Length == 1 ? "" : args[1]);
			}
		}
	}
}

