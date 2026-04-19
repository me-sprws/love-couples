using LoveCouples.Domain.Contracts;

namespace CouplesService.Domain.Entities;

public abstract class Account : Entity
{
    public string Username { get; set; }
    public string ExternalId { get; set; }
    public User User { get; set; }
    public Guid UserId { get; set; }
}

public abstract class EmailAccount : Account
{
    public string Email { get; set; }
}

public class GoogleAccount : EmailAccount;

public class ServiceAccount : Account;