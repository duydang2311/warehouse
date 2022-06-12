using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;

namespace Warehouse.Server.Manager.ViewModels;

public class AuthenticationPageViewModel : ObservableObject, IAuthenticationPageViewModel
{
    private string username;
    private string password;
    private string usernameError;
    private string passwordError;
    public string Username
    {
        get => username;
        set => SetProperty(ref username, value);
    }
    public string Password
    {
        get => password;
        set => SetProperty(ref password, value);
    }
    public string UsernameError
    {
        get => usernameError;
        set => SetProperty(ref usernameError, value);
    }
    public string PasswordError
    {
        get => passwordError;
        set => SetProperty(ref passwordError, value);
    }
    public void Login(object sender, RoutedEventArgs e)
    {
    }
}
