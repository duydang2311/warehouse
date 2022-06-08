using Warehouse.Shared.Services;

namespace Warehouse.Shared.Packets;

public interface IPacketHeaderFactory : IServiceFactory<ulong, byte[], IPacketHeader> { }
