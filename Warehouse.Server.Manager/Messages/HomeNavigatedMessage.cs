using Microsoft.Toolkit.Mvvm.Messaging.Messages;

namespace Warehouse.Server.Manager.Messages;

public class HomeNavigatedMessage : ValueChangedMessage<object?>
{
	public HomeNavigatedMessage() : base(null) { }
}
