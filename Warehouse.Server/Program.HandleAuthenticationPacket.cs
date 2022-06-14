using Microsoft.Data.SqlClient;
using Warehouse.Server.SocketHandlers;
using Warehouse.Shared.Packets;

namespace Warehouse.Server;

public partial class Program
{
	private static async Task HandleAuthenticationPacket(ISocketHandler handler, IAuthenticationPacket packet)
	{
		using var connection = await Database.TryGetConnectionAsync(App.RoleAuth);
		if (connection is null)
		{
			return;
		}
		using var cmd = new SqlCommand("udf_TryLoginToStaffAccount", connection);
		cmd.CommandType = System.Data.CommandType.StoredProcedure;
		cmd.Parameters.AddWithValue("@username", packet.Username);
		cmd.Parameters.AddWithValue("@password", packet.Password);
		var ok = (bool)(await cmd.ExecuteScalarAsync())!;
		await handler.Send((await PacketSerializer.TrySerializeAsync(PacketFactory.GetAuthenticationResponsePacket(ok)))!);
	}
}

