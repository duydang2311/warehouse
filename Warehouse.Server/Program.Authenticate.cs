using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Warehouse.Server.Databases;

namespace Warehouse.Server;

public partial class Program
{
	private static void Authenticate()
	{
		var database = Provider.GetRequiredService<IDatabase>();
		Console.WriteLine("Enter password to login: ");
		var password = Console.ReadLine();
		database.ConnectionStringBuilder.Password = password;
		try
		{
			using var connection = new SqlConnection(database.ConnectionStringBuilder.ToString());
			connection.Open();
		}
		catch (SqlException)
		{
			Console.WriteLine("Login failed, exiting the program.");
			Environment.Exit(0);
		}
		Console.WriteLine("Logged in succesfully");
	}
}

