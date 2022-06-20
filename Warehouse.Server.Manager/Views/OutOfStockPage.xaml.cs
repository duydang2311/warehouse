using Microsoft.UI.Xaml.Controls;
using Warehouse.Server.Manager.ViewModels;

namespace Warehouse.Server.Manager.Views;

public sealed partial class OutOfStockPage : Page
{
	public OutOfStockPage(IOutOfStockViewModel vm)
	{
		InitializeComponent();
		DataContext = vm;
	}
}
