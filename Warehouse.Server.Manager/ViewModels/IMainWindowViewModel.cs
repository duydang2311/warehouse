using Microsoft.UI.Xaml.Controls;
using Microsoft.Toolkit.Mvvm.Messaging;
using Warehouse.Server.Manager.Messages;

namespace Warehouse.Server.Manager.ViewModels;

public interface IMainWindowViewModel : IRecipient<DatabaseConnectedMessage>, IRecipient<AuthenticatedMessage>
{
	Page? Content { get; set; }
}
