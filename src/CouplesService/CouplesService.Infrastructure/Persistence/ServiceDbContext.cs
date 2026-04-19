using CouplesService.Domain.Entities;
using LoveCouples.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CouplesService.Infrastructure.Persistence;

public sealed class ServiceDbContext : CoreDbContext
{
    public ServiceDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Couple> Couples => Set<Couple>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Membership> Memberships => Set<Membership>();
    public DbSet<Invitation> Invitations => Set<Invitation>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(ServiceDbContext).Assembly);
    }
}