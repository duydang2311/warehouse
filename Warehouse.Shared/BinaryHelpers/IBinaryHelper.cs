namespace Warehouse.Shared.BinaryHelpers;

public interface IBinaryHelper
{
    byte[] Serialize<T>(T model) where T : class;
    T Deserialize<T>(byte[] bytes) where T : class;
}
