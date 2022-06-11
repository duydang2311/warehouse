namespace Warehouse.Server;

public partial class Program
{
	private static void HelpCommand()
	{
		Console.WriteLine("List of available commands: help, quit, register");
		Console.WriteLine("  > help - list all available commands");
		Console.WriteLine("  > exit - exit the program");
		Console.WriteLine("  > register - register a staff account");
	}
}
