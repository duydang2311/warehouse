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
    private bool ValidateUsername() => username.Length >= 4;
    private bool ValidatePassword() => password.Length >= 8;
    private void UpdateIsEnabled()
    {
        IsEnabled = ValidateUsername() && ValidatePassword();
    }
    private bool ValidateForm()
    {
        if (username.Contains(' '))
        {
            UsernameError = "Username must not contain any spaces";
            return false;
        }
        else if (!ValidateUsername() || username.Contains(' '))
        {
            UsernameError = "Username must be longer than 3 letters";
            return false;
        }
        else if (usernameError.Length != 0)
        {
            UsernameError = "";
        }
        if (!ValidatePassword())
        {
            PasswordError = "Username must be longer than 3 letters";
            return false;
        }
        else if (UsernameError.Length != 0)
        {
            UsernameError = "";
        }
        return true;
    }
    public void Login(object sender, RoutedEventArgs e)
    {
        Username = username.Trim();
        if (!ValidateForm())
        {
            return;
        }
    }
}
