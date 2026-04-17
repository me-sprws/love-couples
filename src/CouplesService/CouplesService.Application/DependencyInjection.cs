using Microsoft.Extensions.DependencyInjection;

namespace CouplesService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(ApplicationLayer).Assembly));
        
        return services;
    }
}