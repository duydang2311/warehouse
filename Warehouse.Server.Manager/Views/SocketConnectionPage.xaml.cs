using Microsoft.UI.Xaml.Controls;
using Warehouse.Server.Manager.Models.Sockets;
using Warehouse.Server.Manager.ViewModels;

namespace Warehouse.Server.Manager.Views;

public sealed partial class SocketConnectionPage : Page
{
    private readonly ISocketConnectionViewModel ViewModel;
    public SocketConnectionPage(ISocketConnectionViewModel vm)
    {
        this.InitializeComponent();
        DataContext = ViewModel = vm;
    }
}
