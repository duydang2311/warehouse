using Microsoft.UI.Xaml.Controls;
using Warehouse.Server.Manager.ViewModels;

namespace Warehouse.Server.Manager.Views;

public sealed partial class InStockPage : Page
{
	public InStockPage(IInStockViewModel vm)
	{
		InitializeComponent();
		DataContext = vm;
	}
}
