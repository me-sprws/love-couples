using CouplesService.Application.Contracts.Responses.Memberships;
using CouplesService.Domain.Entities;

namespace CouplesService.Application.Common.Mappers;

public static class MembershipMappingExtensions
{
    public static MembershipResponse ToMembershipResponse(this Membership membership) =>
        new()
        {
            Id = membership.Id,
            UserId = membership.UserId,
        };
}