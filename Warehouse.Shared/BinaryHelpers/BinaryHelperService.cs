using MessagePack;

namespace Warehouse.Shared.BinaryHelpers;

public class BinaryHelperService
{
    private readonly IBinaryHelper helper;

    public BinaryHelperService(IBinaryHelper helper)
    {
        this.helper = helper;
    }

    public byte[] Serialize<T>(T model) where T : class
    {
        return MessagePackSerializer.Serialize<T>(model);
    }

    public T Deserialize<T>(byte[] bytes) where T : class
    {
        return MessagePackSerializer.Deserialize<T>(bytes);
    }
}
