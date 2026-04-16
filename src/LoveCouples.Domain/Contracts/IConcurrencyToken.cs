namespace LoveCouples.Domain.Contracts;

public interface IConcurrencyToken
{
    byte[] RowVersion { get; set; }
}