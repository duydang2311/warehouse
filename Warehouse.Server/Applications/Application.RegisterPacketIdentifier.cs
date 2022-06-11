using System.Reflection;

namespace Warehouse.Server.Applications;

public sealed partial class Application : IApplication
{
	public void RegisterPacketIdentifier()
	{
		packetIdentifier.Register(Assembly.GetExecutingAssembly());
		foreach (var i in Assembly.GetExecutingAssembly().GetReferencedAssemblies())
		{
			if (i.Name == "Warehouse.Shared")
			{
				packetIdentifier.Register(Assembly.Load(i));
				break;
			}
		}
	}
}

