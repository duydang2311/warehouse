using Microsoft.UI.Xaml.Controls;
using Microsoft.Toolkit.Mvvm.Messaging;
using Warehouse.Server.Manager.Messages;

namespace Warehouse.Server.Manager.ViewModels;

public interface IMainWindowViewModel : IRecipient<SocketConnectedMessage>, IRecipient<AuthenticatedMessage>
{
	Page? Content { get; set; }
}
