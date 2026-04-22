using CouplesService.Domain.Repositories;
using CouplesService.Domain.Services;
using CouplesService.Infrastructure.Configuration;
using CouplesService.Infrastructure.Persistence;
using CouplesService.Infrastructure.Persistence.Repositories;
using CouplesService.Infrastructure.Services;
using LoveCouples.Domain.Services;
using LoveCouples.Infrastructure;
using LoveCouples.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CouplesService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        IConfiguration configuration,
        CodeGeneration codeGeneration)
    {
        services.AddCoreInfrastructure();
        
        services.AddSingleton<ICodeGenerator>(_ => new CodeGenerator(codeGeneration));
        services.AddSingleton<IDateTimeProvider, UtcDateTimeProvider>();
        
        services.AddDbContext<ServiceDbContext>(opt => 
            opt.UseNpgsql(configuration.GetConnectionString("Database")));
        
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<ICouplesRepository, CouplesRepository>();
        services.AddScoped<IAccountsRepository, AccountsRepository>();
        services.AddScoped<IInvitationsRepository, InvitationsRepository>();
        
        return services;
    }
}