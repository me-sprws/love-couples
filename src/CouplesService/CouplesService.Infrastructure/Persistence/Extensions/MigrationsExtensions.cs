using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CouplesService.Infrastructure.Persistence.Extensions;

public static class MigrationsExtensions
{
    public static void ApplyMigrations(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ServiceDbContext>();

        context.Database.Migrate();
    }
}