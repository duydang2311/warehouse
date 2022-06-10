using Microsoft.UI.Xaml;
using Warehouse.Server.Manager.Views;
using Warehouse.Server.Manager.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace Warehouse.Server.Manager;
public sealed partial class MainWindow : Window
{
    public MainWindow(IMainWindowViewModel viewModel, AuthenticationPage page, HomePage page2)
    {
        InitializeComponent();
        ExtendsContentIntoTitleBar = true;
        SetTitleBar(TitleBar);
        viewModel.Content = page;
        Frame.DataContext = viewModel;
    }
}
