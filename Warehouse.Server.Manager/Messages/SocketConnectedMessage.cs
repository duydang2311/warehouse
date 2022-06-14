using Microsoft.Toolkit.Mvvm.Messaging.Messages;

namespace Warehouse.Server.Manager.Messages;

public class SocketConnectedMessage : ValueChangedMessage<object?>
{
    public SocketConnectedMessage() : base(null) { }
}
