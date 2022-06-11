using Warehouse.Server.Commands;

namespace Warehouse.Server;

public partial class Program
{
	private static async Task RegisterCommand(ICommand _, string arg)
	{
		var args = arg.Split(' ', 2);
		if (args.Length != 2)
		{
			Console.WriteLine("> register [username] [password]");
			return;
		}
		if (await App.RegisterAccount(args[0], args[1]))
		{
			Console.WriteLine($"Account {args[0]} has been registered");
			return;
		}
		Console.WriteLine($"Account {args[0]} was not registered");
	}
}
