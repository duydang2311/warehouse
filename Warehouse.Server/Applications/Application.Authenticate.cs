using Microsoft.Data.SqlClient;
using System.Text;

namespace Warehouse.Server.Applications;

public sealed partial class Application : IApplication
{
	private static string ReadPassword()
	{
		var builder = new StringBuilder(32);
		ConsoleKeyInfo info;
		while (true)
		{
			info = Console.ReadKey(true);
			if (info.Key == ConsoleKey.Enter)
			{
				Console.Write(Environment.NewLine);
				break;
			}
			if (info.Key != ConsoleKey.Backspace)
			{
				Console.Write('*');
				builder.Append(info.KeyChar);
				continue;
			}
			if (builder.Length != 0)
			{
				Console.Write("\b \b");
				builder.Remove(builder.Length - 1, 1);
			}
		} while (info.Key != ConsoleKey.Enter) ;
		return builder.ToString();
	}
	public bool TryAuthenticateDatabase()
	{
		Console.WriteLine("SQL connection authentication");
		Console.Write("Enter login: ");
		var login = Console.ReadLine();
		Console.Write("Enter password: ");
		var password = ReadPassword();
		database.ConnectionStringBuilder.UserID = login;
		database.ConnectionStringBuilder.Password = password;
		try
		{
			using var connection = new SqlConnection(database.ConnectionStringBuilder.ToString());
			connection.Open();
		}
		catch (SqlException)
		{
#if DEBUG
			throw;
#else
			return false;
#endif
		}
		return true;
	}
	public bool TryAuthenticateRole()
	{
		Console.Write($"Application role authentication{Environment.NewLine}Enter role name: ");
		var name = Console.ReadLine();
		Console.Write("Enter role password: ");
		var password = ReadPassword();
		try
		{
			RoleAuth.Name = name!.Trim();
			RoleAuth.Password = password.Trim();
			using var connection = database.TryGetConnection(RoleAuth);
			if (connection is null)
			{
				return false;
			}
		}
		catch (SqlException)
		{
#if DEBUG
			throw;
#else
			return false;
#endif
		}
		return true;
	}
}
