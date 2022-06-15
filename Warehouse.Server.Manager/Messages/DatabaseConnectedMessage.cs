using Microsoft.Toolkit.Mvvm.Messaging.Messages;

namespace Warehouse.Server.Manager.Messages;

public class DatabaseConnectedMessage : ValueChangedMessage<object?>
{
    public DatabaseConnectedMessage() : base(null) { }
}
