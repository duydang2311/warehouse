using MessagePack;

namespace Warehouse.Shared.Packets;

[Union(0, typeof(ClientPacketData))]
public interface IClientPacketData : IPacketData
{
    ClientPacketType Type { get; }
}

public enum ClientPacketType : byte
{
    Select
}
