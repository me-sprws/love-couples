using CouplesService.Domain.Entities;
using CouplesService.Domain.Tests.Builders;
using CouplesService.Domain.ValueObjects;
using FluentResults;
using LoveCouples.Domain.Services;
using Moq;

namespace CouplesService.Domain.Tests;

public class CoupleTests
{
    readonly Mock<IDateTimeProvider> _time = new();
    
    [Fact]
    public void EnterRelationship_EnterOneMember_AddedOneMemberAndStatusUpdatedAndTogetherDateNullAndSeparatedNull()
    {
        var couple = Couple.CreateEmpty();

        var result = couple.EnterRelationship(MembershipBuilder.Build(), _time.Object);
        
        Assert.True(result.IsSuccess);
        Assert.Single(couple.Memberships);
        Assert.Equal(CouplesStatus.Alone, couple.Status);
        Assert.Null(couple.TogetherSince);
        Assert.Null(couple.SeparatedAt);
    }
    
    [Fact]
    public void EnterRelationship_EnterTwoMembersIntoCouple_AddedTwoMembersAndStatusUpdatedAndTogetherDateUpdatedIfNull()
    {
        var couple = Couple.CreateEmpty();
        
        var resultFirst = couple.EnterRelationship(MembershipBuilder.Build(), _time.Object);
        var resultSecond = couple.EnterRelationship(MembershipBuilder.Build(), _time.Object);
        
        Assert.True(resultFirst.IsSuccess);
        Assert.True(resultSecond.IsSuccess);
        Assert.Equal(2, couple.Memberships.Count);
        Assert.Equal(CouplesStatus.Dating, couple.Status);
        Assert.Equal(_time.Object.Now, couple.TogetherSince);
        Assert.Null(couple.SeparatedAt);
    }
    
    [Fact]
    public void EnterRelationship_EnterMoreThanMaxMembers_ReturnsFailureCreationResult()
    {
        var couple = Couple.CreateEmpty();
        
        Result? lastResult = null;
        for (var i = 0; i <= Couple.MaxMembers + 1; i++)
        {
            lastResult = couple.EnterRelationship(MembershipBuilder.Build(), _time.Object);
        }
        
        Assert.NotNull(lastResult);
        Assert.True(lastResult.IsFailed);
    }
}