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
	private static void HelpCommand()
	{
		Console.WriteLine("List of available commands: help, quit, register");
		Console.WriteLine("  > help - list all available commands");
		Console.WriteLine("  > exit - exit the program");
		Console.WriteLine("  > register - register a staff account");
	}
	private static void ExitCommand()
	{
		Environment.Exit(0);
	}
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
