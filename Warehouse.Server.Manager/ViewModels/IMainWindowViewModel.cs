using Microsoft.UI.Xaml.Controls;
using Microsoft.Toolkit.Mvvm.Messaging;
using Warehouse.Server.Manager.Messages;

namespace Warehouse.Server.Manager.ViewModels;

public interface IMainWindowViewModel : IRecipient<SocketConnectedMessage>
{
    Page? Content { get; set; }
}
