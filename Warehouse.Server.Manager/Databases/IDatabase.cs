using Microsoft.Data.SqlClient;

namespace Warehouse.Server.Manager.Databases;

public interface IDatabase
{
	SqlConnectionStringBuilder ConnectionStringBuilder { get; }
	SqlConnection GetConnection(IRoleAuth auth);
	Task<SqlConnection> GetConnectionAsync(IRoleAuth auth);
	SqlConnection? TryGetConnection(IRoleAuth auth);
	Task<SqlConnection?> TryGetConnectionAsync(IRoleAuth auth);
}
