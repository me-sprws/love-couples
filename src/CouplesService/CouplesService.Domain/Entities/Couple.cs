using CouplesService.Domain.Services;
using CouplesService.Domain.ValueObjects;
using FluentResults;
using LoveCouples.Domain.Contracts;
using LoveCouples.Domain.Services;

namespace CouplesService.Domain.Entities;

public sealed class Couple : Entity
{
    public CouplesStatus Status { get; set; }
    public DateTimeOffset? SeparatedAt { get; set; }
    public DateTimeOffset? TogetherSince { get; set; }
    public Invitation? Invitation { get; set; }
    public ICollection<Membership> Memberships { get; set; } = new List<Membership>();

    public Result EnterRelationship(Membership membership, IDateTimeProvider dateTimeProvider)
    {
        if (Memberships.Count >= 2)
        {
            return Result.Fail("You cannot have more than two relationships.");
        }
        
        Memberships.Add(membership);

        Status = CouplesStatus.Dating;
        TogetherSince ??= dateTimeProvider.Now;

        return Result.Ok();
    }
    
    public Result Separate(Func<Membership, bool> predicate, IDateTimeProvider dateTimeProvider)
    {
        return Separate(Memberships.Where(predicate).FirstOrDefault(), dateTimeProvider);
    }

    public Result Separate(Membership? initiator, IDateTimeProvider dateTimeProvider)
    {
        if (initiator is null || !Memberships.Remove(initiator))
        {
            return Result.Fail("Initiator does not in this couple.");
        }

        Status = CouplesStatus.Separated;
        SeparatedAt = dateTimeProvider.Now;

        return Result.Ok();
    }
}