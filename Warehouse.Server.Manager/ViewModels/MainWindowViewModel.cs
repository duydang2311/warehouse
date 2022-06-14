using Microsoft.UI.Xaml.Controls;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;
using Warehouse.Server.Manager.Messages;
using Warehouse.Server.Manager.Views;

namespace Warehouse.Server.Manager.ViewModels;

public class MainWindowViewModel : ObservableObject, IMainWindowViewModel
{
    private readonly SocketConnectionPage socketConnectionPage;
    private readonly AuthenticationPage authenticationPage;
    private readonly HomePage homePage;
    private Page? content;
    public Page? Content
    {
        get => content;
        set => SetProperty(ref content, value);
    }
    public MainWindowViewModel(
        SocketConnectionPage socketConnectionPage,
        AuthenticationPage authenticationPage,
        HomePage homePage
    )
    {
        this.socketConnectionPage = socketConnectionPage;
        this.authenticationPage = authenticationPage;
        this.homePage = homePage;
        WeakReferenceMessenger.Default.Register(this);
    }
    public void Receive(SocketConnectedMessage sender)
    {
        Content = authenticationPage;
    }
}
