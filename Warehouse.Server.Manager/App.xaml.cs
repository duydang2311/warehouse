using Microsoft.UI.Xaml;
using Microsoft.Extensions.DependencyInjection;
using System;
using Warehouse.Server.Manager.Views;
using Warehouse.Server.Manager.ViewModels;
using System.Diagnostics;

namespace Warehouse.Server.Manager;

public sealed partial class App : Application
{
    private static ServiceCollection serviceCollection;
    public new static App Current => (App)Application.Current;
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
            .AddSingleton<IHomePageViewModel, HomePageViewModel>()
            .AddSingleton<HomePage>()
            .AddSingleton<MainWindow>()
            .AddSingleton<IMainWindowViewModel, MainWindowViewModel>()
            .AddSingleton<AuthenticationPage>();
        return serviceCollection.BuildServiceProvider();
    }
    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        var mainWindow = Services.GetRequiredService<MainWindow>();
        mainWindow.Activate();
    }
}


