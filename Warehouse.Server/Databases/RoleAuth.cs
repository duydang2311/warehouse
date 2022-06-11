namespace Warehouse.Server.Databases;

public class RoleAuth : IRoleAuth
{
	public string Name { get; set; } = "";
	public string Password { get; set; } = "";
}
