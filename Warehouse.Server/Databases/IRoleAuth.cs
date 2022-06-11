namespace Warehouse.Server.Databases;

public interface IRoleAuth
{
	string Name { get; }
	string Password { get; }
}
