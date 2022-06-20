using Microsoft.UI.Xaml.Media;

namespace Warehouse.Server.Manager.Models;

public interface IProductItemModel
{
	long Id { get; }
	string Name { get; }
	string Brand { get; }
	string Category { get; }
	ImageSource Image { get; }
	string PriceFormat { get; }
	string CategoryBrandFormat { get; }
	string QuantityFormat { get; }
	decimal Price { get; }
	int Quantity { get; }
}
