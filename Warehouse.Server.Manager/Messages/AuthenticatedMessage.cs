using Microsoft.Toolkit.Mvvm.Messaging.Messages;

namespace Warehouse.Server.Manager.Messages;

public class AuthenticatedMessage : ValueChangedMessage<object?>
{
    public AuthenticatedMessage() : base(null) { }
}
