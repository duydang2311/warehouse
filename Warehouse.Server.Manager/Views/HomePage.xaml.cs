using Microsoft.UI.Xaml.Controls;
using Warehouse.Server.Manager.ViewModels;

namespace Warehouse.Server.Manager.Views;

public sealed partial class HomePage : Page
{
	private readonly IHomePageViewModel vm;
	public HomePage(IHomePageViewModel vm)
	{
		DataContext = this.vm = vm;
		InitializeComponent();
	}
}
