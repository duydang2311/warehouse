using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using System.Net.Sockets;
using Warehouse.Server.Manager.Models.Sockets;
using Warehouse.Shared.Packets;
using Warehouse.Shared.Packets.Serializers;

namespace Warehouse.Server.Manager.ViewModels;

public class AuthenticationPageViewModel : ObservableObject, IAuthenticationPageViewModel
{
	private readonly IPacketFactory packetFactory;
	private readonly IPacketSerializer packetSerializer;
	private readonly ISocket socket;
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
	public AuthenticationPageViewModel(ISocket socket, IPacketFactory packetFactory, IPacketSerializer packetSerializer)
	{
		this.socket = socket;
		this.packetFactory = packetFactory;
		this.packetSerializer = packetSerializer;
		isEnabled = false;
		username = password = usernameError = passwordError = "";
	}
	private bool ValidateUsername() => Username.Length >= 4;
	private bool ValidatePassword() => Password.Length >= 8;
	private void UpdateIsEnabled()
	{
		IsEnabled = ValidateUsername() && ValidatePassword();
	}
	private bool ValidateForm()
	{
		PasswordError = "";
		UsernameError = "";
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
	public async void Login(object sender, RoutedEventArgs e)
	{
		Username = Username.Trim();
		if (!ValidateForm())
		{
			return;
		}
		var header = await packetSerializer.TrySerializeAsync(packetFactory.GetAuthenticationPacket(Username, Password));
		if (header is null)
		{
			PasswordError = "Could not serialize authentication packet";
			return;
		}
		var result = await socket.Send(header);
		if (result.ErrorCode != SocketError.Success)
		{
			PasswordError = $"Authentication packet sent but failed somehow ({result.ErrorCode})";
			return;
		}
		System.Diagnostics.Debug.WriteLine("OK");
	}
}
