using Microsoft.Data.SqlClient;
using Warehouse.Shared.Packets;
using Warehouse.Shared.TcpServers;

namespace Warehouse.Server;

public partial class Program
{
	private static async Task HandleAuthenticationPacket(TcpSession sender, IAuthenticationPacket packet)
	{
		Console.WriteLine("handle auth packet");
		using var connection = await Database.TryGetConnectionAsync(App.RoleAuth);
		if (connection is null)
		{
			return;
		}
		using var cmd = new SqlCommand("udf_TryLoginToStaffAccount", connection);
		cmd.CommandType = System.Data.CommandType.StoredProcedure;
		cmd.Parameters.AddWithValue("@username", packet.Username);
		cmd.Parameters.AddWithValue("@password", packet.Password);
		Console.WriteLine($"{packet.Username}, {packet.Password}");
		// var ok = (bool)(await cmd.ExecuteScalarAsync())!;
		var ok = true;
		await sender.SendAsync(PacketSerializer.TrySerialize(PacketFactory.GetAuthenticationResponsePacket(ok))!);
	}
}

