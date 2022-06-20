using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Warehouse.Server.Manager.Models;
using Warehouse.Server.Manager.Databases;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;
using Warehouse.Server.Manager.Messages;
using Microsoft.UI.Xaml.Media.Imaging;

namespace Warehouse.Server.Manager.ViewModels;

public class HomeViewModel : ObservableObject, IHomeViewModel
{
	private readonly IDatabase database;
	private readonly IRoleAuth roleAuth;
	private LinkedList<IProductItemModel> products;
	public LinkedList<IProductItemModel> Products
	{
		get => products;
		set => SetProperty(ref products, value);
	}
	public HomeViewModel(IDatabase database, IRoleAuth roleAuth)
	{
		this.database = database;
		this.roleAuth = roleAuth;
		products = new LinkedList<IProductItemModel>();
		WeakReferenceMessenger.Default.Register(this);
	}
	public async void Receive(HomeNavigatedMessage message)
	{
		using var connection = await database.TryGetConnectionAsync(roleAuth);
		if (connection is null)
		{
			return;
		}
		using var cmd = new SqlCommand("select * from VI_ProductsList", connection);
		using var reader = cmd.ExecuteReader();
		var list = new LinkedList<IProductItemModel>();
		while (reader.Read())
		{
			list.AddLast(new ProductItemModel
			{
				Id = (long)reader["Product ID"],
				Brand = (string)reader["Brand Name"],
				Category = (string)reader["Category Name"],
				Name = (string)reader["Product Name"],
				Quantity = (int)reader["Quantity"],
				Price = (decimal)reader["Unit Price"],
				Image = new BitmapImage(new Uri((string)reader["ImageUrl"]))
			});
		}
		Products = list;
	}
}