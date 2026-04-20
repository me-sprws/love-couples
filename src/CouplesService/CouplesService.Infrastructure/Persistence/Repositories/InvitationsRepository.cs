using CouplesService.Domain.Entities;
using CouplesService.Domain.Repositories;
using LoveCouples.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CouplesService.Infrastructure.Persistence.Repositories;

public sealed class InvitationsRepository(ServiceDbContext dbContext) 
    : Repository<Invitation, ServiceDbContext>(dbContext), IInvitationsRepository
{
    public IQueryable<Invitation> Get(GetInvitationsOptions options)
    {
        var query = QueryableSet;

        if (options.InvitationId is { } invitationId)
        {
            query = query.Where(x => x.Id == invitationId);
        }
        
        if (options.CoupleId is { } coupleId)
        {
            query = query.Where(x => x.CoupleId == coupleId);
        }
        
        if (options.Code is { } code)
        {
            query = query.Where(x => x.Code == code);
        }

        if (options.IncludeCouple)
        {
            query = query.Include(x => x.Couple);
        }
        
        if (options.IncludeCoupleMembers)
        {
            query = query.Include(x => x.Couple)
                         .ThenInclude(x => x.Memberships);
        }
        
        if (options.AsNoTracking)
        {
            query = query.AsNoTracking();
        }

        return query;
    }
}