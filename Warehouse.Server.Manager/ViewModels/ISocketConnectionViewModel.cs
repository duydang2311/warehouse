using Microsoft.UI.Xaml;
using System;

namespace Warehouse.Server.Manager.ViewModels;

public interface ISocketConnectionViewModel
{
    string Hostname { get; set; }
    string Port { get; set; }
    string Error { get; set; }
    void Connect(object sender, RoutedEventArgs e);
}
