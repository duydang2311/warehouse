using Microsoft.Data.SqlClient;

namespace Warehouse.Server.Databases;

public interface IDatabase
{
	SqlConnectionStringBuilder ConnectionStringBuilder { get; }
	SqlConnection? TryGetConnection(IRoleAuth auth);
	Task<SqlConnection?> TryGetConnectionAsync(IRoleAuth auth);
}
