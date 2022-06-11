namespace Warehouse.Server.Databases;

public class RoleAuthFactory : IRoleAuthFactory
{
	public IRoleAuth GetService(string name, string password) => new RoleAuth(name, password);
}
