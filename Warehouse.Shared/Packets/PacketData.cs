using MessagePack;

namespace Warehouse.Shared.Packets;

[MessagePackObject(true)]
public abstract class PacketData : IPacketData { }
