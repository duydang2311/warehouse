using Microsoft.Data.SqlClient;

namespace Warehouse.Server.Databases;

public interface IDatabase
{
	SqlConnectionStringBuilder ConnectionStringBuilder { get; }
}
