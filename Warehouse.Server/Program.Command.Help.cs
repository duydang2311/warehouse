using Warehouse.Server.Commands;

namespace Warehouse.Server;

public partial class Program
{
	private static void HelpCommand(ICommand _0, string _1)
	{
		foreach (var pair in App.Commands)
		{
			Console.WriteLine($"> {pair.Key} - {pair.Value.Description}");
		}
	}
}
