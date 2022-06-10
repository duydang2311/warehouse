using System.Net.Sockets;

namespace Warehouse.Shared.Sockets;

public interface ISocketOperationResult
{
	int Bytes { get; }
	SocketError ErrorCode { get; }
}

public struct SocketOperationResult : ISocketOperationResult
{
	public int Bytes { get; init; }
	public SocketError ErrorCode { get; init; }

	public SocketOperationResult(int bytes, SocketError errorCode)
	{
		Bytes = bytes;
		ErrorCode = errorCode;
	}
}
