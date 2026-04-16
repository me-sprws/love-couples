using LoveCouples.Domain.Contracts;

namespace CouplesService.Domain.Entities;

public class Membership : Entity
{
    public User User { get; set; }
    public Guid UserId { get; set; }
    public Couples Couples { get; set; }
    public Guid CouplesId { get; set; }
}