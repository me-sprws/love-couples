using CouplesService.Application.Contracts.Responses.Memberships;
using CouplesService.Domain.ValueObjects;

namespace CouplesService.Application.Contracts.Responses.Couples;

public sealed class CoupleResponse
{
    public Guid Id { get; set; }
    public CouplesStatus Status { get; set; }
    public DateTimeOffset TogetherSince { get; set; }
    public DateTimeOffset? SeparatedAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public List<MembershipResponse> Members { get; set; }
}