using CouplesService.Application.Contracts.Responses.Invitations;
using CouplesService.Domain.Entities;

namespace CouplesService.Application.Common.Mappers;

public static class InvitationMappingExtensions
{
    public static InvitationResponse ToInvitationResponse(this Invitation invitation) =>
        new()
        {
            CoupleId = invitation.CoupleId,
            Code = invitation.Code,
        };
}