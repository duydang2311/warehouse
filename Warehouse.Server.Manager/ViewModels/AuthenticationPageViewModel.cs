using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;

namespace Warehouse.Server.Manager.ViewModels;

public class AuthenticationPageViewModel : ObservableObject, IAuthenticationPageViewModel
{
    private string username;
    private string password;
    private string usernameError;
    private string passwordError;
    private bool isEnabled = false;
    public string Username
    {
        get => username;
        set
        {
            SetProperty(ref username, value);
            UpdateIsEnabled();
        }
    }
    public string Password
    {
        get => password;
        set
        {
            SetProperty(ref password, value);
            UpdateIsEnabled();
        }
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
    public bool IsEnabled
    {
        get => isEnabled;
        set => SetProperty(ref isEnabled, value);
    }
    public AuthenticationPageViewModel()
    {
        isEnabled = false;
        username = password = usernameError = passwordError = "";
    }
    private void UpdateIsEnabled()
    {
        IsEnabled = true;
        if (username.Length < 4
        || password.Length < 8)
        {
            IsEnabled = false;
            return;
        }
    }
    public void Login(object sender, RoutedEventArgs e)
    {
        Username = username.Trim();
        UpdateIsEnabled();
        if (isEnabled)
        {
            // implement: send auth
        }
    }
}
