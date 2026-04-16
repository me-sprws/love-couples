using LoveCouples.Domain.Contracts;

namespace CouplesService.Domain.Entities;

public class Invitation : Entity
{
    public string Code { get; set; }
    public Couples Couples { get; set; }
    public Guid CouplesId { get; set; }
    public User Sender { get; set; }
    public Guid SenderId { get; set; }
}