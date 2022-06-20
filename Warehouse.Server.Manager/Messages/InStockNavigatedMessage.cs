using Microsoft.Toolkit.Mvvm.Messaging.Messages;

namespace Warehouse.Server.Manager.Messages;

public class InStockNavigatedMessage : ValueChangedMessage<object?>
{
	public InStockNavigatedMessage() : base(null) { }
}
