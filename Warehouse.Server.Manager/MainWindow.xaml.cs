using Microsoft.UI.Xaml;
using Warehouse.Server.Manager.ViewModels;

namespace Warehouse.Server.Manager;
public sealed partial class MainWindow : Window
{
	public MainWindow(IMainWindowViewModel vm)
	{
		InitializeComponent();
		Grid.DataContext = vm;
		ExtendsContentIntoTitleBar = true;
		SetTitleBar(TitleBar);
	}
}
