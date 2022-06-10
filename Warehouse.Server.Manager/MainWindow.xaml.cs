using Microsoft.UI.Xaml;
using Warehouse.Server.Manager.Views;
using Warehouse.Server.Manager.ViewModels;

namespace Warehouse.Server.Manager;
public sealed partial class MainWindow : Window
{
    public MainWindow(IMainWindowViewModel viewModel, AuthenticationPage page)
    {
        InitializeComponent();
        ExtendsContentIntoTitleBar = true;
        SetTitleBar(TitleBar);
        Frame.DataContext = viewModel;
        viewModel.Content = page;
    }
}
