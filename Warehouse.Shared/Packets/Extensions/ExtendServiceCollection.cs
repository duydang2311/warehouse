using Warehouse.Shared.Packets;
using Warehouse.Shared.Packets.Identifiers;
using Warehouse.Shared.Packets.Serializers;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class ExtendServiceCollection
{
    public static IServiceCollection WithPackets(this IServiceCollection self)
    {
        self.AddSingleton<IPacketFactory, PacketFactory>()
            .AddSingleton<IPacketDataIdentifier, PacketDataIdentifier>()
            .AddSingleton<IPacketDataSerializer, PacketDataSerializer>();
        return self;
    }
}
