namespace Warehouse.Server;

public partial class Program
{
	private static void RegisterCommand(string arg)
	{
		var args = arg.Split(' ', 2);
		if (args.Length != 2)
		{
			Console.WriteLine("> register [username] [password]");
			return;
		}
		Console.WriteLine($"An account with username {args[0]} has been registered");
	}
}
