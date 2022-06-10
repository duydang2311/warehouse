using Microsoft.UI.Xaml.Controls;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Warehouse.Server.Manager.ViewModels;

public interface IHomePageViewModel
{
	NavigationViewItem SelectedNavViewItem { get; set; }
	string NavViewHeader { get; }
}

public class HomePageViewModel : ObservableObject, IHomePageViewModel
{
	private NavigationViewItem selectedNavViewItem;
	private string navViewHeader;
	public string NavViewHeader
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
			NavViewHeader = (string)selectedNavViewItem.Tag;
		}
	}
}
