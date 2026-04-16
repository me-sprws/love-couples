using LoveCouples.Domain.Contracts;
using LoveCouples.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace LoveCouples.Infrastructure.Persistence;

public abstract class CoreDbContext : DbContext, IUnitOfWork
{
    IDbContextTransaction? _transaction;

    protected CoreDbContext()
    {
    }

    protected CoreDbContext(DbContextOptions options) : base(options)
    {
    }

    public async Task<IDisposable> BeginTransactionAsync(CancellationToken ctk = default)
    {
        if (_transaction is not null)
            throw new InvalidOperationException("You can have only one active transaction");

        return _transaction = await Database.BeginTransactionAsync(ctk);
    }

    public Task CommitTransactionAsync(CancellationToken ctk = default)
    {
        if (_transaction is null)
            throw new InvalidOperationException("You must call BeginTransactionAsync() before CommitTransaction()");

        return _transaction.CommitAsync(ctk);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateEntries();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        UpdateEntries();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new())
    {
        UpdateEntries();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override int SaveChanges()
    {
        UpdateEntries();
        return base.SaveChanges();
    }

    void UpdateEntries()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is IAuditable auditable)
                UpdateAuditable(entry, auditable);

            if (entry.Entity is IConcurrencyToken token)
                UpdateConcurrencyToken(token);
        }
    }

    static void UpdateAuditable(EntityEntry entry, IAuditable auditable)
    {
        if (entry.State is EntityState.Added)
        {
            auditable.CreatedAt = DateTimeOffset.UtcNow;
            auditable.UpdatedAt = DateTimeOffset.UtcNow;
        }

        if (entry.State is EntityState.Modified) auditable.UpdatedAt = DateTimeOffset.UtcNow;
    }

    static void UpdateConcurrencyToken(IConcurrencyToken token)
    {
        token.RowVersion = Guid.NewGuid().ToByteArray();
    }
}