using Microsoft.UI.Xaml;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;
using Warehouse.Server.Manager.Messages;
using Warehouse.Shared.TcpClients;

namespace Warehouse.Server.Manager.ViewModels;

public class SocketConnectionViewModel : ObservableObject, ISocketConnectionViewModel
{
	private readonly ITcpClientFactory tcpClientFactory;
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
	public SocketConnectionViewModel(ITcpClientFactory tcpClientFactory)
	{
		this.tcpClientFactory = tcpClientFactory;
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
	public void Connect(object sender, RoutedEventArgs e)
	{
		if (!ValidateForm())
		{
			return;
		}
		var client = tcpClientFactory.GetService(hostname, int.Parse(port));
		if (!client.Connect())
		{
			Error = $"Could not connect to {hostname}:{port}";
			return;
		}
		WeakReferenceMessenger.Default.Send(new SocketConnectedMessage());
	}
}
