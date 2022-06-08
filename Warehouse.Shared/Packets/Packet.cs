using MessagePack;

namespace Warehouse.Shared.Packets;

[MessagePackObject(true)]
public abstract class Packet : IPacket { }
