using MessagePack;

namespace Warehouse.Shared.Packets;

[Union(0, typeof(PacketData))]
public interface IPacketData { }
