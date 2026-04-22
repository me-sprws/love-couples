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
    IDateTimeProvider time
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
            return Result.Fail("Invalid invitation code.");

        var acceptInvitation = invitation.Couple.AcceptInvitation(request.InviteeUserId, time);

        if (acceptInvitation.IsSuccess)
            await invitationsRepository.UnitOfWork.SaveChangesAsync(ctk);
        
        return acceptInvitation;
    }
}