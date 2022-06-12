using Microsoft.Data.SqlClient;

namespace Warehouse.Server.Databases;

public class Database : IDatabase
{
	public SqlConnectionStringBuilder ConnectionStringBuilder { get; }
	public Database()
	{
		ConnectionStringBuilder = new SqlConnectionStringBuilder()
		{
			DataSource = "DUYDANG",
			InitialCatalog = "Warehouse",
			TrustServerCertificate = true
		};
	}
	public SqlConnection GetConnection(IRoleAuth auth)
	{
		var connection = new SqlConnection(ConnectionStringBuilder.ToString());
		connection.Open();
		using var cmd = new SqlCommand("sp_setapprole", connection);
		cmd.CommandType = System.Data.CommandType.StoredProcedure;
		cmd.Parameters.AddWithValue("@rolename", auth.Name);
		cmd.Parameters.AddWithValue("@password", auth.Password);
		cmd.ExecuteNonQuery();
		return connection;
	}
	public async Task<SqlConnection> GetConnectionAsync(IRoleAuth auth)
	{
		var connection = new SqlConnection(ConnectionStringBuilder.ToString());
		await connection.OpenAsync();
		using var cmd = new SqlCommand("sp_setapprole", connection);
		cmd.CommandType = System.Data.CommandType.StoredProcedure;
		cmd.Parameters.AddWithValue("@rolename", auth.Name);
		cmd.Parameters.AddWithValue("@password", auth.Password);
		await cmd.ExecuteNonQueryAsync();
		return connection;
	}
	public SqlConnection? TryGetConnection(IRoleAuth auth)
	{
		var connection = new SqlConnection(ConnectionStringBuilder.ToString());
		try
		{
			connection.Open();
			using var cmd = new SqlCommand("sp_setapprole", connection);
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@rolename", auth.Name);
			cmd.Parameters.AddWithValue("@password", auth.Password);
			cmd.ExecuteNonQuery();
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
		var connection = new SqlConnection(ConnectionStringBuilder.ToString());
		try
		{
			await connection.OpenAsync();
			using var cmd = new SqlCommand("sp_setapprole", connection);
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@rolename", auth.Name);
			cmd.Parameters.AddWithValue("@password", auth.Password);
			await cmd.ExecuteNonQueryAsync();
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

