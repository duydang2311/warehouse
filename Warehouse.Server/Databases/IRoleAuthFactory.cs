using Warehouse.Shared.Services;

namespace Warehouse.Server.Databases;

public interface IRoleAuthFactory : IServiceFactory<string, string, IRoleAuth> { }
