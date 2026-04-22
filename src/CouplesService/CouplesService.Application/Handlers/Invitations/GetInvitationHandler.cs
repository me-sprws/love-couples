using CouplesService.Application.Commands.Invitations;
using CouplesService.Application.Common.Mappers;
using CouplesService.Application.Contracts.Responses.Invitations;
using CouplesService.Domain.Repositories;
using CouplesService.Domain.Services;
using FluentResults;
using MediatR;

namespace CouplesService.Application.Handlers.Invitations;

public sealed class GetInvitationHandler(
    IUsersRepository usersRepository,
    IInvitationsRepository invitationRepository,
    ICouplesRepository couplesRepository,
    ICodeGenerator codeGenerator
) : IRequestHandler<GetInvitationCommand, Result<InvitationResponse>>
{
    public async Task<Result<InvitationResponse>> Handle(GetInvitationCommand request, CancellationToken ctk)
    {
        var invitation = await invitationRepository.FirstOrDefaultAsync(
            invitationRepository.Get(new(
                CoupleId: request.CoupleId,
                AsNoTracking: true)), 
            ctk);

        if (invitation is not null)
            return invitation.ToInvitationResponse();
        
        var couple = await couplesRepository.FirstOrDefaultAsync(
            couplesRepository.Get(new(CoupleId: request.CoupleId)), 
            ctk);

        if (couple is null)
            return Result.Fail<InvitationResponse>("Couple not found.");

        var sender = await usersRepository.FirstOrDefaultAsync(
            usersRepository.QueryableSet, 
            request.SenderId, 
            ctk);

        if (sender is null)
            return Result.Fail<InvitationResponse>("Sender not found.");
        
        var invitationResult = couple.CreateInvitation(sender, codeGenerator);

        if (invitationResult.IsSuccess)
            await couplesRepository.UnitOfWork.SaveChangesAsync(ctk);
        
        return invitationResult.Map(x => x.ToInvitationResponse());
    }
}