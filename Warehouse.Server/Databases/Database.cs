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
			UserID = "warehouse_login_test",
			InitialCatalog = "Warehouse",
			TrustServerCertificate = true
		};
	}
}

