using Microsoft.UI.Xaml.Controls;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;
using Warehouse.Server.Manager.Views;
using Warehouse.Server.Manager.Messages;

namespace Warehouse.Server.Manager.ViewModels;

public class MainViewModel : ObservableObject, IMainViewModel
{
	private NavigationViewItem selectedNavViewItem;
	private string navViewHeader;
	public string Header
	{
		get => navViewHeader;
		set => SetProperty(ref navViewHeader, value);
	}
	public NavigationViewItem SelectedNavViewItem
	{
		get => selectedNavViewItem;
		set
		{
			SetProperty(ref selectedNavViewItem, value);
			Header = (string)selectedNavViewItem.Content;
			if (selectedNavViewItem.Tag is InStockPage)
			{
				WeakReferenceMessenger.Default.Send<InStockNavigatedMessage>();
			}
			else if (selectedNavViewItem.Tag is HomePage)
			{
				WeakReferenceMessenger.Default.Send<HomeNavigatedMessage>();
			}
			else if (selectedNavViewItem.Tag is OutOfStockPage)
			{
				WeakReferenceMessenger.Default.Send<OutOfStockNavigatedMessage>();
			}
		}
	}
	public Page HomeTag { get; }
	public Page InStockTag { get; }
	public Page OutOfStockTag { get; }
	public MainViewModel(HomePage homePage, InStockPage inStockPage, OutOfStockPage outOfStockPage)
	{
		HomeTag = homePage;
		InStockTag = inStockPage;
		OutOfStockTag = outOfStockPage;
		navViewHeader = "";
		selectedNavViewItem = null!;
	}
}
