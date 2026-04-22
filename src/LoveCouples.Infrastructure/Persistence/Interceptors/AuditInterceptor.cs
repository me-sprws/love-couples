using LoveCouples.Domain.Contracts;
using LoveCouples.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LoveCouples.Infrastructure.Persistence.Interceptors;

public sealed class AuditInterceptor(
    IDateTimeProvider dateTimeProvider
) : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, 
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null)
            throw new InvalidOperationException($"{nameof(eventData.Context)} cannot be null.");
        
        UpdateEntries(eventData.Context);
        
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    
    void UpdateEntries(DbContext context)
    {
        foreach (var entry in context.ChangeTracker.Entries()
                     .Where(e => e.State is EntityState.Added or EntityState.Modified))
        {
            if (entry.Entity is IAuditable auditable)
                UpdateAuditable(entry, auditable);

            if (entry.Entity is IConcurrencyToken token)
                UpdateConcurrencyToken(token);
        }
    }

    void UpdateAuditable(EntityEntry entry, IAuditable auditable)
    {
        if (entry.State is EntityState.Added)
        {
            auditable.CreatedAt = dateTimeProvider.Now;
            auditable.UpdatedAt = dateTimeProvider.Now;
        }

        if (entry.State is EntityState.Modified) auditable.UpdatedAt = dateTimeProvider.Now;
    }

    static void UpdateConcurrencyToken(IConcurrencyToken token)
    {
        token.RowVersion = Guid.NewGuid().ToByteArray();
    }
}