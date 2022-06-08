namespace Warehouse.Shared.Packets.Identifiers;

public interface IPacketIdentifier
{
    ulong TryIdentify<T>() where T : IPacket;
    Type? TryIdentify(ulong identity);
    bool Is<T>(IPacketHeader packet) where T : IPacket;
}
