using System.Reflection;

namespace Warehouse.Shared.Packets.Identifiers;

public class PacketIdentifier : IPacketIdentifier
{
	private readonly Dictionary<Type, ulong> identityDict = new();
	private ulong increment;

	public PacketIdentifier()
	{
		increment = (ulong)new Random().NextInt64() + 1;
		Register(Assembly.GetExecutingAssembly());
		var entry = Assembly.GetEntryAssembly();
		if (entry is null)
		{
			return;
		}
		Register(entry);
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
		Console.WriteLine(packet.Identity + " vs" + TryIdentify<T>());
		return packet.Identity != 0 && packet.Identity == TryIdentify<T>();
	}

	public void Register(Assembly assembly)
	{
		foreach (var type in assembly.GetTypes())
		{
			if (type.IsInterface && type.GetInterface(typeof(IPacket).FullName!) is not null)
			{
				identityDict.Add(type, increment++);
			}
		}
		foreach (var type in assembly.GetTypes())
		{
			if (type.IsClass)
			{
				foreach (var i in identityDict)
				{
					if (i.Key is not IPacket && type.IsAssignableTo(i.Key))
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
