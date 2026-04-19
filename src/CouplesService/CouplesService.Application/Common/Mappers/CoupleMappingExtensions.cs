using CouplesService.Application.Contracts.Responses.Couples;
using CouplesService.Domain.Entities;

namespace CouplesService.Application.Common.Mappers;

public static class CoupleMappingExtensions
{
    public static CoupleResponse ToCoupleResponse(this Couple couple) =>
        new()
        {
            Id = couple.Id,
            Status = couple.Status,
            TogetherSince = couple.TogetherSince,
            SeparatedAt = couple.SeparatedAt,
            CreatedAt = couple.CreatedAt,
            Members = couple.Memberships
                .Select(member => 
                    member.ToMembershipResponse()).ToList()
        };
}