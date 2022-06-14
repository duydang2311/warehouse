using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.IO;
using System.Net.Sockets;
using Warehouse.Shared.Sockets.Clients;
using Warehouse.Shared.Packets;
using Warehouse.Shared.Packets.Serializers;
using Warehouse.Shared.Packets.Identifiers;
using Warehouse.Server.Manager.Messages;

namespace Warehouse.Server.Manager.ViewModels;

public class AuthenticationPageViewModel : ObservableObject, IAuthenticationPageViewModel
{
	private readonly IPacketFactory packetFactory;
	private readonly IClientSocket socket;
	private readonly IPacketSerializer packetSerializer;
	private readonly IPacketIdentifier packetIdentifier;
	private string username;
	private string password;
	private string usernameError;
	private string passwordError;
	private bool isEnabled = false;
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
	public bool IsEnabled
	{
		get => isEnabled;
		set => SetProperty(ref isEnabled, value);
	}
	public AuthenticationPageViewModel(IClientSocket socket, IPacketFactory packetFactory, IPacketSerializer packetSerializer, IPacketIdentifier packetIdentifier)
	{
		this.socket = socket;
		this.packetFactory = packetFactory;
		this.packetSerializer = packetSerializer;
		this.packetIdentifier = packetIdentifier;
		isEnabled = false;
		username = password = usernameError = passwordError = "";
		socket.Received += AuthenticationResponded;
	}
	private bool ValidateUsername() => Username.Length >= 4;
	private bool ValidatePassword() => Password.Length >= 8;
	private bool ValidateForm()
	{
		PasswordError = "";
		UsernameError = "";
		if (username.Contains(' '))
		{
			UsernameError = "Username must not have any spaces";
			return false;
		}
		else if (!ValidateUsername())
		{
			UsernameError = "Username must have more than 3 letters";
			return false;
		}
		if (!ValidatePassword())
		{
			PasswordError = "Password must have more than 7 letters";
			return false;
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
	}
	private async void AuthenticationResponded(IClientSocket sender, IPacketHeader header)
	{
		if (packetIdentifier.Is<IAuthenticationResponsePacket>(header))
		{
			var packet = await packetSerializer.TryDeserializeAsync<IAuthenticationResponsePacket>(header);
			if (packet is null || !packet.Ok)
			{
				App.Current.Window.DispatcherQueue.TryEnqueue(() =>
				{
					PasswordError = "Failed to login";
				});
				return;
			}
			socket.Received -= AuthenticationResponded;
			WeakReferenceMessenger.Default.Send<AuthenticatedMessage>();
		}
	}
}
