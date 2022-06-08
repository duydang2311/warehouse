namespace Warehouse.Shared.Packets.Identifiers;

public interface IPacketDataIdentifier
{
    ulong TryIdentify<T>() where T : IPacketData;
    Type? TryIdentify(ulong identity);
    bool Is<T>(IPacket packet) where T : IPacketData;
}
