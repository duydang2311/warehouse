using Warehouse.Server.Commands;

namespace Warehouse.Server;

public partial class Program
{
	private static void ExitCommand(ICommand _0, string _1)
	{
		Environment.Exit(0);
	}
}
