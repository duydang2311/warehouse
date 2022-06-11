using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Warehouse.Server.SocketListeners;
using Warehouse.Server.SocketHandlers;
using Warehouse.Shared.Packets;
using Warehouse.Shared.Packets.Identifiers;

namespace Warehouse.Server;

public partial class Program
{
	private static void ReadCommand()
	{
		while (true)
		{
			var line = Console.ReadLine()?.Trim();
			if (line is null)
			{
				continue;
			}
			var args = line.Split(' ', 2);
			args[0] = args[0].ToLower();
			if (args[0] == "help")
			{
				HelpCommand();
				continue;
			}
			if (args[0] == "exit")
			{
				ExitCommand();
				continue;
			}
			if (args[0] == "register")
			{
				if (args.Length == 1)
				{
				}
				RegisterCommand(args.Length == 1 ? "" : args[1]);
				continue;
			}
		}
	}
}
