using Microsoft.UI.Xaml.Controls;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Warehouse.Server.Manager.ViewModels;

public interface IMainWindowViewModel
{
    Page Content { get; set; }
}
