using Microsoft.Data.SqlClient;

namespace Warehouse.Server.Applications;

public sealed partial class Application : IApplication
{
	public async Task<bool> RegisterAccount(string username, string password)
	{
		using var connection = await database.TryGetConnectionAsync(RoleAuth);
		if (connection is null)
		{
			return false;
		}
		using var cmd = new SqlCommand("usp_InsertStaffAccount", connection);
		cmd.CommandType = System.Data.CommandType.StoredProcedure;
		cmd.Parameters.AddWithValue("@username", username);
		cmd.Parameters.AddWithValue("@password", password);
		try
		{
			return await cmd.ExecuteNonQueryAsync() == 1;
		}
		catch
		{
#if DEBUG
			throw;
#else
			return false;
#endif
		}
	}
}
