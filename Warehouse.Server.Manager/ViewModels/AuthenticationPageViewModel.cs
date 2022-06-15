using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Data.SqlClient;
using Warehouse.Server.Manager.Databases;
using Warehouse.Server.Manager.Messages;


namespace Warehouse.Server.Manager.ViewModels;

public class AuthenticationPageViewModel : ObservableObject, IAuthenticationPageViewModel
{
	private readonly IDatabase database;
	private readonly IRoleAuth roleAuth;
	private string username;
	private string password;
	private string usernameError;
	private string passwordError;
	private object buttonContent;
	public string Username
	{
		get => username;
		set => SetProperty(ref username, value);
	}
	public string Password
	{
		get => password;
		set => SetProperty(ref password, value);
	}
	public string UsernameError
	{
		get => usernameError;
		set => SetProperty(ref usernameError, value);
	}
	public string PasswordError
	{
		get => passwordError;
		set => SetProperty(ref passwordError, value);
	}
	public object ButtonContent
	{
		get => buttonContent;
		set => SetProperty(ref buttonContent, value);
	}
	public AuthenticationPageViewModel(IDatabase database, IRoleAuth roleAuth)
	{
		this.database = database;
		this.roleAuth = roleAuth;
		username = password = usernameError = passwordError = "";
		buttonContent = "Login";
	}
	private bool ValidateForm()
	{
		PasswordError = "";
		UsernameError = "";
		if (Username.Length == 0)
		{
			UsernameError = "Username cannot be blank";
			return false;
		}
		if (Password.Length == 0)
		{
			PasswordError = "Password cannot be blank";
			return false;
		}
		return true;
	}
	public async void Login(object sender, RoutedEventArgs e)
	{
		Username = Username.Trim();
		if (!ValidateForm())
		{
			return;
		}
		ButtonContent = new ProgressRing { Width = 25, Height = 25 };
		using var connection = await database.TryGetConnectionAsync(roleAuth);
		if (connection is null)
		{
			ButtonContent = "Login";
			PasswordError = "Cannot establish connection to database";
			return;
		}
		System.Diagnostics.Debug.WriteLine(Username + " " + Password);
		using var cmd = new SqlCommand("select dbo.udf_TryLoginToStaffAccount(@name, @password)", connection);
		cmd.Parameters.AddWithValue("@name", Username);
		cmd.Parameters.AddWithValue("@password", Password);
		var ok = (bool)(await cmd.ExecuteScalarAsync())!;
		ButtonContent = "Login";
		if (!ok)
		{
			PasswordError = "Wrong credentials, could not login to any staff account";
			return;
		}
		WeakReferenceMessenger.Default.Send<AuthenticatedMessage>();
	}
}
