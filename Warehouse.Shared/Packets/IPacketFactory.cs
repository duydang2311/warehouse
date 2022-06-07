using Warehouse.Shared.Services;

namespace Warehouse.Shared.Packets;

public interface IPacketFactory : IServiceFactory<ulong, byte[], IPacket> { }
