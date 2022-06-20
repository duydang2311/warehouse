using Microsoft.UI.Xaml.Controls;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Warehouse.Server.Manager.Views;
using Warehouse.Server.Manager.Models;

namespace Warehouse.Server.Manager.ViewModels;

public interface IMainViewModel
{
	NavigationViewItem SelectedNavViewItem { get; set; }
	string Header { get; }
	Page HomeTag { get; }
	Page InStockTag { get; }
	Page OutOfStockTag { get; }
}

