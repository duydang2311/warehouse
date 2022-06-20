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
			TrustServerCertificate = true
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
		using var cmdsetapprole = new SqlCommand("sp_setapprole", connection);
		cmdsetapprole.CommandType = System.Data.CommandType.StoredProcedure;
		cmdsetapprole.Parameters.AddWithValue("@rolename", auth.Name);
		cmdsetapprole.Parameters.AddWithValue("@password", auth.Password);
		cmdsetapprole.ExecuteNonQuery();
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
		using var cmdsetapprole = new SqlCommand("sp_setapprole", connection);
		cmdsetapprole.CommandType = System.Data.CommandType.StoredProcedure;
		cmdsetapprole.Parameters.AddWithValue("@rolename", auth.Name);
		cmdsetapprole.Parameters.AddWithValue("@password", auth.Password);
		await cmdsetapprole.ExecuteNonQueryAsync();
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
			using var cmdsetapprole = new SqlCommand("sp_setapprole", connection);
			cmdsetapprole.CommandType = System.Data.CommandType.StoredProcedure;
			cmdsetapprole.Parameters.AddWithValue("@rolename", auth.Name);
			cmdsetapprole.Parameters.AddWithValue("@password", auth.Password);
			cmdsetapprole.ExecuteNonQuery();
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
			System.Diagnostics.Debug.WriteLine("Before");
			var cmd = new SqlCommand("select dbo.udf_TryLoginToApplicationRoles(@rolename, @password)", connection);
			System.Diagnostics.Debug.WriteLine("After");
			cmd.Parameters.AddWithValue("@rolename", auth.Name);
			cmd.Parameters.AddWithValue("@password", auth.Password);
			if (!(bool)(await cmd.ExecuteScalarAsync())!)
			{
				return null;
			}
			cmd.Dispose();
			using var cmdsetapprole = new SqlCommand("sp_setapprole", connection);
			cmdsetapprole.CommandType = System.Data.CommandType.StoredProcedure;
			cmdsetapprole.Parameters.AddWithValue("@rolename", auth.Name);
			cmdsetapprole.Parameters.AddWithValue("@password", auth.Password);
			await cmdsetapprole.ExecuteNonQueryAsync();
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

