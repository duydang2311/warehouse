using Warehouse.Server.Commands;

namespace Warehouse.Server.Applications;

public sealed partial class Application : IApplication
{
	private readonly Dictionary<string, ICommand> commandDict = new();
	private void InitCommand()
	{
		commandDict.Add("help", commandFactory.GetService("help", "List all available commands", (_0, _1) =>
		{
			foreach (var pair in commandDict)
			{
				Console.WriteLine($"> {pair.Key} - {pair.Value.Description}");
			}
		}));
	}
	public bool TryAddCommand(ICommand command)
	{
		return commandDict.TryAdd(command.Name, command);
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
			if (commandDict.ContainsKey(args[0]))
			{
				var command = commandDict[args[0]];
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

