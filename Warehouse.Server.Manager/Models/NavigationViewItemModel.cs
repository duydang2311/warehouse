using Microsoft.UI.Xaml.Controls;

namespace Warehouse.Server.Manager.Models;

public class NavigationViewItemModel : INavigationViewItemModel
{
	public bool Selected { get; set; }
	public int Tag { get; init; }
	public IconElement Icon { get; init; }
	public string Content { get; init; }
	public Page AssociatedPage { get; init; }
	public NavigationViewItemModel()
	{
		Selected = false;
		Tag = 0;
		Content = "";
		Icon = new SymbolIcon(Symbol.Home);
		AssociatedPage = null!;
	}
}
