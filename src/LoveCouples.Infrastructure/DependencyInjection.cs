using LoveCouples.Domain.Services;
using LoveCouples.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LoveCouples.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, UtcDateTimeProvider>();

        return services;
    }
}