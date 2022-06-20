using Microsoft.Toolkit.Mvvm.Messaging;
using Warehouse.Server.Manager.Models;
using Warehouse.Server.Manager.Messages;

namespace Warehouse.Server.Manager.ViewModels;

public interface IHomeViewModel : IRecipient<HomeNavigatedMessage>
{
	LinkedList<IProductItemModel> Products { get; }
}
