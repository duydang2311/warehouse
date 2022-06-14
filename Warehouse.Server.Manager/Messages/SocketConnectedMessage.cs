using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using Warehouse.Server.Manager.ViewModels;

namespace Warehouse.Server.Manager.Messages;

public class SocketConnectedMessage : ValueChangedMessage<object?>
{
    public SocketConnectedMessage() : base(null) { }
}
