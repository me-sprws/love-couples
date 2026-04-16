namespace LoveCouples.Domain.Contracts;

public abstract class Entity : IHasKey<Guid>, IAuditable
{
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public Guid Id { get; set; }
}