using CouplesService.Application.Commands.Invitations;
using CouplesService.Domain.Repositories;
using CouplesService.Domain.Services;
using CouplesService.Domain.ValueObjects;
using FluentResults;
using LoveCouples.Domain.Services;
using MediatR;

namespace CouplesService.Application.Handlers.Invitations;

public sealed class AcceptInvitationHandler(
    IInvitationsRepository invitationsRepository,
    IDateTimeProvider dateTimeProvider
) : IRequestHandler<AcceptInvitationCommand, Result>
{
    public async Task<Result> Handle(AcceptInvitationCommand request, CancellationToken ctk)
    {
        var invitation = await invitationsRepository.FirstOrDefaultAsync(
            invitationsRepository.Get(new(
                Code: request.InvitationCode,
                IncludeCouple: true,
                IncludeCoupleMembers: true)), 
            ctk);

        if (invitation is null)
        {
            return Result.Fail("Invalid invitation code.");
        }

        if (invitation.Couple.Memberships.Any(x => x.UserId == request.InviteeUserId))
        {
            return Result.Fail("Invitee user already in couple.");
        }

        var enterRelationship = invitation.Couple.EnterRelationship(new()
        {
            UserId = request.InviteeUserId
        }, dateTimeProvider);

        if (enterRelationship.IsFailed)
            return enterRelationship;
        
        await invitationsRepository.DeleteAsync(invitation, ctk);
        await invitationsRepository.UnitOfWork.SaveChangesAsync(ctk);
        
        return Result.Ok();
    }
}