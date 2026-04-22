using LoveCouples.Domain.Services;

namespace LoveCouples.Infrastructure.Services;

public sealed class UtcDateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset Now => DateTimeOffset.UtcNow;
}