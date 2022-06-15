using Microsoft.UI.Xaml;
using System;

namespace Warehouse.Server.Manager.ViewModels;

public interface IDatabaseConnectionViewModel
{
	string Server { get; set; }
	string ServerError { get; set; }
	string Login { get; set; }
	string LoginError { get; set; }
	string Password { get; set; }
	string PasswordError { get; set; }
	object ButtonContent { get; set; }
	void Connect(object sender, RoutedEventArgs e);
}
