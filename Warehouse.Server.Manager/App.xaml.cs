using Microsoft.UI.Xaml;
using Microsoft.Extensions.DependencyInjection;
using System;
using Warehouse.Server.Manager.Views;
using Warehouse.Server.Manager.ViewModels;
using Warehouse.Server.Manager.Databases;

namespace Warehouse.Server.Manager;

public sealed partial class App : Application
{
	private static IServiceCollection services = null!;
	public new static App Current => (App)Application.Current;
	public MainWindow Window => Provider.GetRequiredService<MainWindow>();
	public IServiceProvider Provider { get; }
	public App()
	{
		Provider = ConfigureServices();
		InitializeComponent();
	}
	private static IServiceProvider ConfigureServices()
	{
		services = new ServiceCollection()
			.WithDatabases()
			.AddSingleton<MainWindow>()
			.AddSingleton<DatabaseConnectionPage>()
			.AddSingleton<AuthenticationPage>()
			.AddSingleton<MainPage>()
			.AddSingleton<HomePage>()
			.AddSingleton<InStockPage>()
			.AddSingleton<OutOfStockPage>()
			.AddSingleton<IMainWindowViewModel, MainWindowViewModel>()
			.AddSingleton<IDatabaseConnectionViewModel, DatabaseConnectionViewModel>()
			.AddSingleton<IAuthenticationPageViewModel, AuthenticationPageViewModel>()
			.AddSingleton<IMainViewModel, MainViewModel>()
			.AddSingleton<IInStockViewModel, InStockViewModel>();
		return services.BuildServiceProvider();
	}
	protected override void OnLaunched(LaunchActivatedEventArgs args)
	{
		var mainWindow = Provider.GetRequiredService<MainWindow>();
		mainWindow.Activate();
	}
}


