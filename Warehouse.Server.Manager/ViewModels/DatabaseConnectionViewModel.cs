using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;
using Warehouse.Shared.TcpClients;
using Warehouse.Server.Manager.Messages;
using Warehouse.Server.Manager.Databases;

namespace Warehouse.Server.Manager.ViewModels;

public class DatabaseConnectionViewModel : ObservableObject, IDatabaseConnectionViewModel
{
	private readonly IDatabase database;
	private readonly IRoleAuth roleAuth;
	private string server;
	private string login;
	private string password;
	private string serverError;
	private string loginError;
	private string passwordError;
	private object buttonContent;
	public string Server
	{
		get => server;
		set => SetProperty(ref server, value);
	}
	public string Login
	{
		get => login;
		set => SetProperty(ref login, value);
	}
	public string Password
	{
		get => password;
		set => SetProperty(ref password, value);
	}
	public string ServerError
	{
		get => serverError;
		set => SetProperty(ref serverError, value);
	}
	public string LoginError
	{
		get => loginError;
		set => SetProperty(ref loginError, value);
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
	public DatabaseConnectionViewModel(IDatabase database, IRoleAuth roleAuth)
	{
		this.database = database;
		this.roleAuth = roleAuth;
		server = "";
		password = "";
		login = "";
		serverError = "";
		loginError = "";
		passwordError = "";
		buttonContent = "Connect";
	}
	private bool ValidateForm()
	{
		Server = server.Trim();
		ServerError = "";
		LoginError = "";
		PasswordError = "";
		if (server.Length == 0)
		{
			ServerError = "Server cannot be blank";
			return false;
		}
		Login = login.Trim();
		if (login.Length == 0)
		{
			LoginError = "Login name cannot be blank";
			return false;
		}
		if (Password.Length == 0)
		{
			PasswordError = "Login password cannot be blank";
			return false;
		}
		return true;
	}
	public async void Connect(object sender, RoutedEventArgs e)
	{
		//Server = "DUYDANG";
		//Login = "gearstore";
		//Password = "12345";
		Server = Server.Trim();
		Login = Login.Trim();
		Password = Password.Trim();
		if (!ValidateForm())
		{
			return;
		}
		database.ConnectionStringBuilder.DataSource = server;
		database.ConnectionStringBuilder.UserID = login;
		database.ConnectionStringBuilder.Password = password;
		PasswordError = "Connecting to the database...";
		ButtonContent = new ProgressRing() { Height = 25, Width = 25 };
		using var connection = await database.TryGetConnectionAsync(roleAuth).ConfigureAwait(true);
		if (connection is null)
		{
			ButtonContent = "Connect";
			PasswordError = "Wrong credentials, database connection failed";
			return;
		}
		PasswordError = "";
		WeakReferenceMessenger.Default.Send(new DatabaseConnectedMessage());
	}
}
