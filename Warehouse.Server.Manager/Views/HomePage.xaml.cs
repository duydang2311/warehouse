using Microsoft.UI.Xaml.Controls;
using Warehouse.Server.Manager.ViewModels;

namespace Warehouse.Server.Manager.Views;

public sealed partial class HomePage : Page
{
	public HomePage(IHomeViewModel vm)
	{
		InitializeComponent();
		DataContext = vm;
	}
}
