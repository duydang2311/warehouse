using System.Reflection;

namespace Warehouse.Shared.Packets.Identifiers;

public class PacketIdentifier : IPacketIdentifier
{
    private readonly Dictionary<Type, ulong> identityDict = new();
    private ulong increment;

    public PacketIdentifier()
    {
        increment = (ulong)new Random().NextInt64() + 1;
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

    public void Register(Assembly assembly)
    {
        foreach (var type in assembly.GetTypes())
        {
            if (type.IsInterface && type.GetInterface(typeof(IPacket).FullName!) is not null)
            {
                identityDict.Add(type, (ulong)increment++);
            }
        }
        foreach (var type in assembly.GetTypes())
        {
            if (type.IsClass)
            {
                foreach (var i in identityDict)
                {
                    if (type.IsAssignableTo(i.Key))
                    {
                        identityDict.Add(type, i.Value);
                        break;
                    }
                }
            }
        }
    }

    public void Register(Type type)
    {
        identityDict.Add(type, (ulong)increment++);
    }
}
