using Microsoft.UI.Xaml;
using Warehouse.Server.Manager.Views;

namespace Warehouse.Server.Manager;
public sealed partial class MainWindow : Window
{
	public MainWindow(HomePage page)
	{
		InitializeComponent();
		Title = "Warehouse Manager";
		Content = page;
	}
}
