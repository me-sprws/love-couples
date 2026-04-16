using LoveCouples.Domain.Contracts;

namespace CouplesService.Domain.Entities;

public sealed class Invitation : Entity
{
    public string Code { get; set; }
    public Couple Couple { get; set; }
    public Guid CoupleId { get; set; }
    public User Sender { get; set; }
    public Guid SenderId { get; set; }
}