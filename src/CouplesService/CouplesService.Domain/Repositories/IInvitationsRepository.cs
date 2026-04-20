using CouplesService.Domain.Entities;
using LoveCouples.Domain.Repositories;

namespace CouplesService.Domain.Repositories;

public sealed record GetInvitationsOptions(
    Guid? InvitationId = null,
    Guid? CoupleId = null,
    string? Code = null,
    bool IncludeCouple = false,
    bool IncludeCoupleMembers = false,
    bool AsNoTracking = false
);

public interface IInvitationsRepository : IRepository<Invitation>
{
    IQueryable<Invitation> Get(GetInvitationsOptions options);
}