using CouplesService.Domain.Repositories;
using CouplesService.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CouplesService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<ICouplesRepository, CouplesRepository>();
        services.AddScoped<IInvitationsRepository, InvitationsRepository>();
        
        return services;
    }
}