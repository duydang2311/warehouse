using Microsoft.Data.SqlClient;

namespace Warehouse.Server.Manager.Databases;

public class Database : IDatabase
{
	public SqlConnectionStringBuilder ConnectionStringBuilder { get; }
	public Database()
	{
		ConnectionStringBuilder = new SqlConnectionStringBuilder()
		{
			InitialCatalog = "GearStore",
			TrustServerCertificate = true,
		};
	}
	public SqlConnection GetConnection(IRoleAuth auth)
	{
		var connection = new SqlConnection(ConnectionStringBuilder.ToString());
		connection.Open();
		using var cmd = new SqlCommand("select dbo.udf_TryLoginToApplicationRoles(@rolename, @password)", connection);
		cmd.Parameters.AddWithValue("@rolename", auth.Name);
		cmd.Parameters.AddWithValue("@password", auth.Password);
		var ok = (bool)cmd.ExecuteScalar();
		if (!ok)
		{
			throw new ArgumentException("Wrong credentials");
		}
		return connection;
	}
	public async Task<SqlConnection> GetConnectionAsync(IRoleAuth auth)
	{
		var connection = new SqlConnection(ConnectionStringBuilder.ToString());
		await connection.OpenAsync();
		using var cmd = new SqlCommand("select dbo.udf_TryLoginToApplicationRoles(@rolename, @password)", connection);
		cmd.Parameters.AddWithValue("@rolename", auth.Name);
		cmd.Parameters.AddWithValue("@password", auth.Password);
		if (!(bool)(await cmd.ExecuteScalarAsync())!)
		{
			throw new ArgumentException("Wrong credentials");
		}
		return connection;
	}
	public SqlConnection? TryGetConnection(IRoleAuth auth)
	{
		SqlConnection connection;
		try
		{
			connection = new SqlConnection(ConnectionStringBuilder.ToString());
		}
		catch
		{
			return default;
		}
		try
		{
			connection.Open();
			using var cmd = new SqlCommand("select dbo.udf_TryLoginToApplicationRoles(@rolename, @password)", connection);
			cmd.Parameters.AddWithValue("@rolename", auth.Name);
			cmd.Parameters.AddWithValue("@password", auth.Password);
			if (!(bool)cmd.ExecuteScalar())
			{
				return null;
			}
			return connection;
		}
		catch
		{
			connection.Close();
			connection.Dispose();
			return default;
		}
	}
	public async Task<SqlConnection?> TryGetConnectionAsync(IRoleAuth auth)
	{
		SqlConnection connection;
		try
		{
			connection = new SqlConnection(ConnectionStringBuilder.ToString());
		}
		catch
		{
			return default;
		}
		try
		{
			await connection.OpenAsync();
			using var cmd = new SqlCommand("select dbo.udf_TryLoginToApplicationRoles(@rolename, @password)", connection);
			cmd.Parameters.AddWithValue("@rolename", auth.Name);
			cmd.Parameters.AddWithValue("@password", auth.Password);
			if (!(bool)(await cmd.ExecuteScalarAsync())!)
			{
				return null;
			}
			return connection;
		}
		catch
		{
			await connection.CloseAsync();
			connection.Dispose();
			return default;
		}
	}
}

