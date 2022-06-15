using Microsoft.UI.Xaml.Controls;
using Warehouse.Server.Manager.ViewModels;

namespace Warehouse.Server.Manager.Views;

public sealed partial class DatabaseConnectionPage : Page
{
	private readonly IDatabaseConnectionViewModel ViewModel;
	public DatabaseConnectionPage(IDatabaseConnectionViewModel vm)
	{
		this.InitializeComponent();
		DataContext = ViewModel = vm;
	}
}
