using Microsoft.UI.Xaml.Controls;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;
using Warehouse.Server.Manager.Messages;
using Warehouse.Server.Manager.Views;

namespace Warehouse.Server.Manager.ViewModels;

public class MainWindowViewModel : ObservableObject, IMainWindowViewModel
{
	private readonly DatabaseConnectionPage databaseConnectionPage;
	private readonly AuthenticationPage authenticationPage;
	private readonly HomePage homePage;
	private Page? content;
	public Page? Content
	{
		get => content;
		set => SetProperty(ref content, value);
	}
	public MainWindowViewModel(
		DatabaseConnectionPage databaseConnectionPage,
		AuthenticationPage authenticationPage,
		HomePage homePage
	)
	{
		this.databaseConnectionPage = databaseConnectionPage;
		this.authenticationPage = authenticationPage;
		this.homePage = homePage;
		Content = databaseConnectionPage;
		WeakReferenceMessenger.Default.Register<DatabaseConnectedMessage>(this);
		WeakReferenceMessenger.Default.Register<AuthenticatedMessage>(this);
	}
	public void Receive(DatabaseConnectedMessage sender)
	{
		Content = authenticationPage;
	}
	public void Receive(AuthenticatedMessage sender)
	{
		Content = homePage;
	}
}
