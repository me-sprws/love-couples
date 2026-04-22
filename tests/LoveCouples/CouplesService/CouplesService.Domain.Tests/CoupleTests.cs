using CouplesService.Domain.Entities;
using CouplesService.Domain.Services;
using CouplesService.Domain.Tests.Builders;
using CouplesService.Domain.ValueObjects;
using FluentResults;
using LoveCouples.Domain.Services;
using Moq;

namespace CouplesService.Domain.Tests;

public class CoupleTests
{
    readonly Mock<IDateTimeProvider> _time = new();
    readonly Mock<ICodeGenerator> _codeGen = new();
    
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
    public void EnterRelationship_EnterTwoMembersWithTogetherPreset_CoupleWithSavedTogetherSince()
    {
        var couple = Couple.CreateEmpty();
        var togetherSince = new DateTimeOffset(2026, 1, 1, 1, 1, 1, TimeSpan.Zero);
        couple.TogetherSince = togetherSince;

        couple.EnterRelationship(MembershipBuilder.Build(), _time.Object);
        couple.EnterRelationship(MembershipBuilder.Build(), _time.Object);
        
        Assert.Equal(togetherSince, couple.TogetherSince);
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
    
    [Fact]
    public void Separate_ReturnsSuccessResultWithSeparatedAtDate()
    {
        var couple = Couple.CreateEmpty();
        var expectedSeparatedAt = _time.Object.Now;
        couple.EnterRelationship(MembershipBuilder.Build(), _time.Object);
        couple.EnterRelationship(MembershipBuilder.Build(), _time.Object);
        
        var result = couple.Separate(couple.Memberships.First(), _time.Object);
        
        Assert.True(result.IsSuccess);
        Assert.Equal(CouplesStatus.Separated, couple.Status);
        Assert.Equal(expectedSeparatedAt, couple.SeparatedAt);
    }
    
    [Fact]
    public void CreateInvitation_CreatesInvitation()
    {
        var couple = Couple.CreateEmpty();

        var result = couple.CreateInvitation(Guid.NewGuid(), _codeGen.Object);
        
        Assert.True(result.IsSuccess);
        Assert.NotNull(couple.Invitation);
    }
    
    [Fact]
    public void AcceptInvitation_InviteeUserAcceptInvitation_SuccessAndRemovedInvitationFromCouple()
    {
        var couple = Couple.CreateEmpty();
        couple.EnterRelationship(MembershipBuilder.Build(), _time.Object);
        couple.CreateInvitation(couple.Memberships.First().UserId, _codeGen.Object);
        
        Assert.NotNull(couple.Invitation);
        var accept = couple.AcceptInvitation(Guid.NewGuid(), _time.Object);
        
        Assert.True(accept.IsSuccess);
        Assert.Null(couple.Invitation);
        Assert.Equal(CouplesStatus.Dating, couple.Status);
        Assert.Equal(2, couple.Memberships.Count);
    }
    
    [Fact]
    public void AcceptInvitation_SenderAcceptInvitation_FailedResult()
    {
        var couple = Couple.CreateEmpty();
        couple.EnterRelationship(MembershipBuilder.Build(), _time.Object);
        couple.CreateInvitation(couple.Memberships.First().UserId, _codeGen.Object);
        
        Assert.NotNull(couple.Invitation);
        var accept = couple.AcceptInvitation(couple.Memberships.First().UserId, _time.Object);
        
        Assert.True(accept.IsFailed);
        Assert.NotNull(couple.Invitation);
        Assert.Single(couple.Memberships);
    }
}