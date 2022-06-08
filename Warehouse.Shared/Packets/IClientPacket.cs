using MessagePack;

namespace Warehouse.Shared.Packets;

[Union(0, typeof(ClientPacket))]
public interface IClientPacket : IPacket
{
    ClientPacketType Type { get; }
}

public enum ClientPacketType : byte
{
    Select
}
