namespace LoveCouples.Domain.Services;

public interface IDateTimeProvider
{
    DateTimeOffset Now { get; }
}