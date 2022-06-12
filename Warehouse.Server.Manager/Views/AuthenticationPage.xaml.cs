using Microsoft.UI.Xaml.Controls;
using Warehouse.Server.Manager.ViewModels;

namespace Warehouse.Server.Manager.Views;

public sealed partial class AuthenticationPage : Page
{
    public IAuthenticationPageViewModel ViewModel { get; }
    public AuthenticationPage(IAuthenticationPageViewModel vm)
    {
        InitializeComponent();
        ViewModel = vm;
    }
}
