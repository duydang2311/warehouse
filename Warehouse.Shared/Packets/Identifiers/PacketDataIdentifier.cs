using System.Reflection;

namespace Warehouse.Shared.Packets.Identifiers;

public class PacketDataIdentifier : IPacketDataIdentifier
{
    private readonly Dictionary<Type, ulong> identityDict = new();

    public PacketDataIdentifier()
    {
        var start = new Random().NextInt64() + 1;
        identityDict.Add(typeof(IPacketData), (ulong)start++);
        foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
        {
            if (type.IsInterface && type.GetInterface(typeof(IPacketData).FullName!) is not null)
            {
                identityDict.Add(type, (ulong)start++);
            }
        }
    }

    public ulong TryIdentify<T>() where T : IPacketData
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
}
