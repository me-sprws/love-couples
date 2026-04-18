namespace CouplesService.WebAPI;

internal static class DependencyInjection
{
    public static IServiceCollection AddWebApi(this IServiceCollection services)
    {
        services.AddOpenApi();
        services.AddControllers();
        
        services.AddAuthorization();
        services.AddAuthentication();
        
        return services;
    }
}