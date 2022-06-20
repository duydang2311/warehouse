using Microsoft.UI.Xaml.Controls;
using Warehouse.Server.Manager.ViewModels;

namespace Warehouse.Server.Manager.Views;

public sealed partial class MainPage : Page
{
	public MainPage(IMainViewModel vm)
	{
		DataContext = vm;
		InitializeComponent();
	}
}
