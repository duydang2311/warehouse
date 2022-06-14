using Microsoft.UI.Xaml;
using Microsoft.Extensions.DependencyInjection;
using System;
using Warehouse.Server.Manager.Views;
using Warehouse.Server.Manager.ViewModels;
using Warehouse.Shared.Sockets.Clients;

namespace Warehouse.Server.Manager;

public sealed partial class App : Application
{
	private static ServiceCollection serviceCollection = null!;
	public new static App Current => (App)Application.Current;
	public MainWindow Window => Services.GetRequiredService<MainWindow>();
	public IServiceProvider Services { get; }
	public App()
	{
		Services = ConfigureServices();
		InitializeComponent();
	}
	private static IServiceProvider ConfigureServices()
	{
		serviceCollection = new ServiceCollection();
		serviceCollection
			.WithSockets()
			.WithPackets()
			.WithSocketClients()
			.AddSingleton<MainWindow>()
			.AddSingleton<SocketConnectionPage>()
			.AddSingleton<AuthenticationPage>()
			.AddSingleton<HomePage>()
			.AddSingleton<IClientSocketFactory, ClientSocketFactory>()
			.AddSingleton<IClientSocket, ClientSocket>(p => (p.GetRequiredService<IClientSocketFactory>().GetService() as ClientSocket)!)
			.AddSingleton<IMainWindowViewModel, MainWindowViewModel>()
			.AddSingleton<ISocketConnectionViewModel, SocketConnectionViewModel>()
			.AddSingleton<IAuthenticationPageViewModel, AuthenticationPageViewModel>()
			.AddSingleton<IHomePageViewModel, HomePageViewModel>();
		return serviceCollection.BuildServiceProvider();
	}
	protected override void OnLaunched(LaunchActivatedEventArgs args)
	{
		var mainWindow = Services.GetRequiredService<MainWindow>();
		mainWindow.Activate();
	}
}


