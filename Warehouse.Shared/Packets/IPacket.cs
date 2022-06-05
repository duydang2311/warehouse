namespace Warehouse.Shared.Packets;

public interface IPacket
{
    ulong Identity { get; }
    byte[] Buffer { get; }
}
