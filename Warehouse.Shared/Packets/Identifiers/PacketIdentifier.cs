using System.Reflection;

namespace Warehouse.Shared.Packets.Identifiers;

public class PacketIdentifier : IPacketIdentifier
{
    private readonly Dictionary<Type, ulong> identityDict = new();

    public PacketIdentifier()
    {
        var start = new Random().NextInt64() + 1;
        identityDict.Add(typeof(IPacket), (ulong)start++);
        foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
        {
            if (type.IsInterface && type.GetInterface(typeof(IPacket).FullName!) is not null)
            {
                identityDict.Add(type, (ulong)start++);
            }
        }
    }

    public ulong TryIdentify<T>() where T : IPacket
    {
        if (identityDict.TryGetValue(typeof(T), out var value))
        {
            return value;
        }
        return 0;
    }

    public Type? TryIdentify(ulong identity)
    {
        foreach (var i in identityDict)
        {
            if (i.Value == identity)
            {
                return i.Key;
            }
        }
        return null;
    }

    public bool Is<T>(IPacketHeader packet) where T : IPacket
    {
        return packet.Identity != 0 && packet.Identity == TryIdentify<T>();
    }
}
