namespace LoveCouples.Domain.Contracts;

public interface IHasKey<TKey>
{
    TKey Id { get; set; }
}