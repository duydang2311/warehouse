using Microsoft.UI.Xaml;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;
using Warehouse.Shared.Sockets.Clients;
using Warehouse.Server.Manager.Messages;

namespace Warehouse.Server.Manager.ViewModels;

public class SocketConnectionViewModel : ObservableObject, ISocketConnectionViewModel
{
	private readonly IClientSocket socket;
	private string hostname;
	private string port;
	private string hostnameError;
	private string error;
	public string Hostname
	{
		get => hostname;
		set => SetProperty(ref hostname, value);
	}
	public string Port
	{
		get => port;
		set => SetProperty(ref port, value);
	}
	public string HostnameError
	{
		get => hostnameError;
		set => SetProperty(ref hostnameError, value);
	}
	public string Error
	{
		get => error;
		set => SetProperty(ref error, value);
	}
	public SocketConnectionViewModel(IClientSocket socket)
	{
		this.socket = socket;
		hostname = "";
		hostnameError = "";
		port = "";
		error = "";
	}
	private bool ValidateForm()
	{
		Hostname = hostname.Trim();
		if (hostname.Length == 0)
		{
			HostnameError = "A hostname is required for connection";
			return false;
		}
		else
		{
			HostnameError = "";
		}
		Port = port.Trim();
		if (port.Length == 0)
		{
			Error = "A port is required for connection";
			return false;
		}
		else
		{
			Error = "";
		}
		if (!int.TryParse(port, out _))
		{
			Error = "Port must be a number";
			return false;
		}
		else
		{
			Error = "";
		}
		return true;
	}
	public async void Connect(object sender, RoutedEventArgs e)
	{
		if (!ValidateForm())
		{
			return;
		}
		var ok = await socket.Connect(hostname, int.Parse(port));
		if (!ok)
		{
			Error = $"Could not connect to {hostname}:{port}";
			return;
		}
		socket.BeginReceive();
		WeakReferenceMessenger.Default.Send(new SocketConnectedMessage());
	}
}
