using Microsoft.UI.Xaml;
using Microsoft.Extensions.DependencyInjection;
using System;
using Warehouse.Server.Manager.Views;
using Warehouse.Server.Manager.ViewModels;

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
			.WithSockets()
			.WithPackets()
			.WithSocketClients()
			.AddSingleton<MainWindow>()
			.AddSingleton<SocketConnectionPage>()
			.AddSingleton<AuthenticationPage>()
			.AddSingleton<HomePage>()
			.AddSingleton<IMainWindowViewModel, MainWindowViewModel>()
			.AddSingleton<ISocketConnectionViewModel, SocketConnectionViewModel>()
			.AddSingleton<IAuthenticationPageViewModel, AuthenticationPageViewModel>()
			.AddSingleton<IHomePageViewModel, HomePageViewModel>();
		return services.BuildServiceProvider();
	}
	protected override void OnLaunched(LaunchActivatedEventArgs args)
	{
		var mainWindow = Provider.GetRequiredService<MainWindow>();
		mainWindow.Activate();
	}
}


