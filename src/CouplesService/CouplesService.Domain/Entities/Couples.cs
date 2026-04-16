using CouplesService.Domain.ValueObjects;
using LoveCouples.Domain.Contracts;

namespace CouplesService.Domain.Entities;

public class Couples : Entity
{
    public CouplesStatus Status { get; set; }
    public DateTimeOffset? SeparatedAt { get; set; }
    public DateTimeOffset TogetherSince { get; set; }
    public ICollection<Membership> Members { get; set; } = new List<Membership>();
}