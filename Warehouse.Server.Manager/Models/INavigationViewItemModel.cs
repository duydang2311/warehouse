using Microsoft.UI.Xaml.Controls;

namespace Warehouse.Server.Manager.Models;

public interface INavigationViewItemModel
{
	bool Selected { get; set; }
	int Tag { get; }
	string Content { get; }
	IconElement Icon { get; }
	Page AssociatedPage { get; }
}
