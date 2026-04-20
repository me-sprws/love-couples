using CouplesService.Application.Commands.Invitations;
using CouplesService.Domain.Repositories;
using CouplesService.Domain.ValueObjects;
using FluentResults;
using MediatR;

namespace CouplesService.Application.Handlers.Invitations;

public sealed class AcceptInvitationHandler(
    IInvitationsRepository invitationsRepository
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

        invitation.Couple.Memberships.Add(new()
        {
            UserId = request.InviteeUserId
        });

        invitation.Couple.Status = CouplesStatus.Dating;
        invitation.Couple.TogetherSince ??= DateTimeOffset.UtcNow;
        
        await invitationsRepository.DeleteAsync(invitation, ctk);

        await invitationsRepository.UnitOfWork.SaveChangesAsync(ctk);
        
        return Result.Ok();
    }
}