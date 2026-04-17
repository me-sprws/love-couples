using CouplesService.Domain.Repositories;
using CouplesService.Infrastructure.Persistence;
using CouplesService.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CouplesService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddDbContext<ServiceDbContext>(opt => 
            opt.UseNpgsql(configuration.GetConnectionString("Database")));
        
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<ICouplesRepository, CouplesRepository>();
        services.AddScoped<IInvitationsRepository, InvitationsRepository>();
        
        return services;
    }
}