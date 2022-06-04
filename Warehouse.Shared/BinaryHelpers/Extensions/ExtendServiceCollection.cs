using Warehouse.Shared.BinaryHelpers;

namespace Microsoft.Extensions.DependencyInjection;

public static class ExtendServiceCollection
{
    public static IServiceCollection WithBinaryHelpers(this IServiceCollection self)
    {
        self
            .AddSingleton<IBinaryHelper, BinaryHelper>()
            .AddSingleton<BinaryHelperService>();
        return self;
    }
}
