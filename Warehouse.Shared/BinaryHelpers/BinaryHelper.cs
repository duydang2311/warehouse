using MessagePack;

namespace Warehouse.Shared.BinaryHelpers;

public class BinaryHelper : IBinaryHelper
{
	public byte[] Serialize<T>(T model) where T : class
	{
		return MessagePackSerializer.Serialize<T>(model);
	}

	public T Deserialize<T>(byte[] buffer) where T : class
	{
		return MessagePackSerializer.Deserialize<T>(buffer);
	}

	public T Deserialize<T>(ReadOnlyMemory<byte> buffer) where T : class
	{
		try
		{
			return MessagePackSerializer.Deserialize<T>(buffer);
		}
		catch
		{
			throw;
		}
	}

	public T Deserialize<T>(byte[] buffer, out int bytesRead) where T : class
	{
		return MessagePackSerializer.Deserialize<T>(buffer, out bytesRead);
	}

	public T Deserialize<T>(ReadOnlyMemory<byte> buffer, out int bytesRead) where T : class
	{
		return MessagePackSerializer.Deserialize<T>(buffer, out bytesRead);
	}
}
