using CouplesService.Domain.Services;
using CouplesService.Domain.ValueObjects;
using FluentResults;
using LoveCouples.Domain.Contracts;
using LoveCouples.Domain.Services;
// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
// ReSharper disable CollectionNeverUpdated.Local

namespace CouplesService.Domain.Entities;

public sealed class Couple : Entity
{
    public const int MaxMembers = 2;

    readonly List<Membership> _memberships = [];

    Couple() { }
    
    public CouplesStatus Status { get; private set; }
    public DateTimeOffset? SeparatedAt { get; private set; }
    public DateTimeOffset? TogetherSince { get; private set; }
    public Invitation? Invitation { get; private set; }
    public IReadOnlyCollection<Membership> Memberships => _memberships;

    public static Couple CreateEmpty()
    {
        return new();
    }

    public static Couple Create(Guid userId, IDateTimeProvider time)
    {
        var couple = new Couple();
        
        couple.EnterRelationship(new()
        {
            UserId = userId,
        }, time);

        return couple;
    }

    public Result<Invitation> CreateInvitation(User sender, ICodeGenerator codeGenerator)
    {
        if (Invitation is not null)
            return Result.Fail("Invitation already exists.");
        
        var invitation = 
            new Invitation
            {
                Couple = this,
                Sender = sender,
                Code = codeGenerator.GenerateCode()
            };
        
        return Result.Ok(invitation);
    }

    public Result EnterRelationship(Membership membership, IDateTimeProvider clock)
    {
        if (membership is null)
            return Result.Fail("Membership is required.");

        if (IsFull())
            return Result.Fail($"Couple cannot have more than {MaxMembers} members.");

        _memberships.Add(membership);
        UpdateOnEnter(clock);

        return Result.Ok();
    }
    
    public Result Separate(Func<Membership, bool> predicate, IDateTimeProvider dateTimeProvider)
    {
        if (Memberships.Where(predicate).FirstOrDefault() is not { } membership)
            return Result.Fail("Membership is required.");
        
        return Separate(membership, dateTimeProvider);
    }

    Result Separate(Membership membership, IDateTimeProvider dateTimeProvider)
    {
        if (membership is null)
            return Result.Fail("Membership is required.");
        
        if (!_memberships.Remove(membership))
            return Result.Fail("Membership not part of the couple.");

        UpdateOnSeparation(dateTimeProvider);

        return Result.Ok();
    }
    
    bool IsFull() => Memberships.Count >= MaxMembers;
    bool IsAlone() => Memberships.Count == 1;

    void UpdateOnEnter(IDateTimeProvider dateTimeProvider)
    {
        if (IsAlone())
        {
            Status = CouplesStatus.Alone;
            return;
        }

        if (IsFull())
        {
            Status = CouplesStatus.Dating;
            TogetherSince ??= dateTimeProvider.Now;
        }
    }
    
    void UpdateOnSeparation(IDateTimeProvider time)
    {
        if (Memberships.Count >= MaxMembers) return;
        
        Status = CouplesStatus.Separated;
        SeparatedAt = time.Now;
    }
}