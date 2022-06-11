using Microsoft.Data.SqlClient;

namespace Warehouse.Server.Applications;

public sealed partial class Application : IApplication
{
	public async Task<bool> RegisterAccount(string username, string password)
	{
		try
		{
			using var connection = await database.GetConnectionAsync(roleAuth);
			using var cmd = new SqlCommand("usp_InsertStaffAccount", connection);
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@username", username);
			cmd.Parameters.AddWithValue("@password", password);
			return await cmd.ExecuteNonQueryAsync() == 1;
		}
		catch
		{
			return false;
		}
	}
}
