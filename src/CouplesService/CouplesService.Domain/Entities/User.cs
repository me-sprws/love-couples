using LoveCouples.Domain.Contracts;

namespace CouplesService.Domain.Entities;

public sealed class User : Entity
{
    public string Name { get; set; }
    public string? Country { get; set; }
    public DateTimeOffset? BirthDate { get; set; }
    public ICollection<Account> Accounts { get; set; } = new List<Account>();
    public ICollection<Membership> Memberships { get; set; } = new List<Membership>();

    public void UpdateInfo(string name, string? country, DateTimeOffset birthDate)
    {
        Name = name;
        Country = country;
        BirthDate = birthDate;
    }
}