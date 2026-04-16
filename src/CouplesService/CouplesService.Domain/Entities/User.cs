using LoveCouples.Domain.Contracts;

namespace CouplesService.Domain.Entities;

public class User : Entity
{
    public string Name { get; set; }
    public string? Country { get; set; }
    public DateTimeOffset BirthDate { get; set; }
    public ICollection<Membership> Memberships { get; set; } = new List<Membership>();
}