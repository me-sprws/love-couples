namespace CouplesService.Domain.ValueObjects;

public enum CouplesStatus : byte
{
    Alone = 0,
    Dating = 2,
    Engaged = 4,
    Married = 8,
    Separated = 16
}