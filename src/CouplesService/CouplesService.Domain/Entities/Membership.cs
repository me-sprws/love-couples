using LoveCouples.Domain.Contracts;

namespace CouplesService.Domain.Entities;

public sealed class Membership : Entity
{
    public User User { get; set; }
    public Guid UserId { get; set; }
    public Couple Couple { get; set; }
    public Guid CoupleId { get; set; }
}