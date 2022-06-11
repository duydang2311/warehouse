using Warehouse.Shared.Services;

namespace Warehouse.Server.Commands;

public interface ICommandFactory : IServiceFactory<string, string, Action<ICommand, string>, ICommand>
{
	IAsyncCommand GetAsyncService(string name, string description, Func<ICommand, string, Task> handler);
}
