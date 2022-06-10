using Microsoft.UI.Xaml.Controls;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Warehouse.Server.Manager.ViewModels;

public class MainWindowViewModel : ObservableObject, IMainWindowViewModel
{
    private Page content;
    public Page Content
    {
        get => content;
        set => SetProperty(ref content, value);
    }
}
