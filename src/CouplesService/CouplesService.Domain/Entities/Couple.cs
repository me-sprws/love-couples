using CouplesService.Domain.ValueObjects;
using LoveCouples.Domain.Contracts;

namespace CouplesService.Domain.Entities;

public sealed class Couple : Entity
{
    public CouplesStatus Status { get; set; }
    public DateTimeOffset? SeparatedAt { get; set; }
    public DateTimeOffset TogetherSince { get; set; }
    public Invitation? Invitation { get; set; }
    public ICollection<Membership> Memberships { get; set; } = new List<Membership>();
}