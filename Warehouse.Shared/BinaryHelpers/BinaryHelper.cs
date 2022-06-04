using MessagePack;

namespace Warehouse.Shared.BinaryHelpers;

public class BinaryHelper : IBinaryHelper
{
    public byte[] Serialize<T>(T model) where T : class
    {
        return MessagePackSerializer.Serialize<T>(model);
    }

    public T Deserialize<T>(byte[] bytes) where T : class
    {
        return MessagePackSerializer.Deserialize<T>(bytes);
    }
}
