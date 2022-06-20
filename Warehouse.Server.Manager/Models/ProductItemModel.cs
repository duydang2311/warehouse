using Microsoft.UI.Xaml.Media;
using System.Globalization;

namespace Warehouse.Server.Manager.Models;

public class ProductItemModel : IProductItemModel
{
	public long Id { get; init; }
	public string Name { get; init; }
	public string Brand { get; init; }
	public string Category { get; init; }
	public ImageSource Image { get; init; }
	public decimal Price { get; init; }
	public int Quantity { get; init; }
	public string PriceFormat { get => string.Format(new CultureInfo("vi-VN"), "{0:C}", Price); }
	public string CategoryBrandFormat { get => $"Hạng mục {Category} - {Brand}"; }
	public string QuantityFormat { get => $"Còn {Quantity} sản phẩm trong kho"; }
	public ProductItemModel()
	{
		Name = "";
		Brand = "";
		Category = "";
		Image = null!;
	}
}
