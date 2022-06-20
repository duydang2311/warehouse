using Microsoft.Toolkit.Mvvm.Messaging;
using Warehouse.Server.Manager.Models;
using Warehouse.Server.Manager.Messages;

namespace Warehouse.Server.Manager.ViewModels;

public interface IOutOfStockViewModel : IRecipient<OutOfStockNavigatedMessage>
{
	LinkedList<IProductItemModel> Products { get; }
}
