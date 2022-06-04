namespace Warehouse.Shared.Services;

public interface IServiceFactory<T1>
{
    T1 GetService();
}

public interface IServiceFactory<T1, T2>
{
    T2 GetService(T1 arg1);
}

public interface IServiceFactory<T1, T2, T3>
{
    T3 GetService(T1 arg1, T2 arg2);
}
