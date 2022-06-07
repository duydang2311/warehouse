namespace Warehouse.Shared.Packets.Identifiers;

public interface IPacketDataIdentifier
{
    ulong TryIdentify<T>() where T : IPacketData;
}
