using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Warehouse.Server.Databases;

namespace Warehouse.Server;

using BCrypt.Net;

public partial class Program
{
	private static IRoleAuth roleAuth = null!;
	private static void Authenticate()
	{
		var database = Provider.GetRequiredService<IDatabase>();
		Console.WriteLine("Connecting to database using SQL connection");
		Console.WriteLine("Enter login: ");
		var login = Console.ReadLine();
		Console.WriteLine("Enter password: ");
		var password = Console.ReadLine();
		database.ConnectionStringBuilder.UserID = login;
		database.ConnectionStringBuilder.Password = password;
		try
		{
			using var connection = new SqlConnection(database.ConnectionStringBuilder.ToString());
			connection.Open();
		}
		catch (SqlException)
		{
			Console.WriteLine("Login failed, exiting the program");
			Environment.Exit(0);
		}
		Console.WriteLine("Enter application role name: ");
		var name = Console.ReadLine();
		Console.WriteLine("Enter application role password: ");
		password = Console.ReadLine();
		try
		{
			using var connection = new SqlConnection(database.ConnectionStringBuilder.ToString());
			connection.Open();
			using var cmd = new SqlCommand("select top 1 Hash from ApplicationRoles where Name = @Name", connection);
			cmd.Parameters.AddWithValue("@Name", name);
			var hash = (string)cmd.ExecuteScalar();
			if (hash is null)
			{
				FailAuthentication();
				return;
			}
			if (!BCrypt.Verify(password, hash))
			{
				FailAuthentication();
				return;
			}
			using var setAppRoleCmd = new SqlCommand("sp_setapprole", connection);
			setAppRoleCmd.CommandType = System.Data.CommandType.StoredProcedure;
			setAppRoleCmd.Parameters.AddWithValue("@rolename", name);
			setAppRoleCmd.Parameters.AddWithValue("@password", hash);
			Console.WriteLine("Authenticated successfully");
			roleAuth = Provider.GetRequiredService<IRoleAuthFactory>().GetService(name!, hash);
		}
		catch (SqlException ex)
		{
			FailAuthentication(ex);
		}
	}
	private static void FailAuthentication(SqlException? ex = null)
	{
		if (ex is not null)
		{
			Console.WriteLine(ex.Message);
		}
		Console.WriteLine("Authentication failed, exiting the program");
		Environment.Exit(0);
	}
}

