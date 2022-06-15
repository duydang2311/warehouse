using Microsoft.UI.Xaml;
using System;

namespace Warehouse.Server.Manager.ViewModels;

public interface IAuthenticationPageViewModel
{
	string Username { get; set; }
	string Password { get; set; }
	string UsernameError { get; set; }
	string PasswordError { get; set; }
	object ButtonContent { get; set; }
	void Login(object sender, RoutedEventArgs e);
}
