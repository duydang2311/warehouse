using Microsoft.Toolkit.Mvvm.Messaging.Messages;

namespace Warehouse.Server.Manager.Messages;

public class OutOfStockNavigatedMessage : ValueChangedMessage<object?>
{
	public OutOfStockNavigatedMessage() : base(null) { }
}
