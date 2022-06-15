namespace Warehouse.Server.Manager.Databases;

public interface IRoleAuth
{
	string Name { get; set; }
	string Password { get; set; }
}
