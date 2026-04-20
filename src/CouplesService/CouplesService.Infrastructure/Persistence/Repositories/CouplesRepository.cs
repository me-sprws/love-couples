using CouplesService.Domain.Entities;
using CouplesService.Domain.Repositories;
using LoveCouples.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CouplesService.Infrastructure.Persistence.Repositories;

public sealed class CouplesRepository(ServiceDbContext dbContext) 
    : Repository<Couple, ServiceDbContext>(dbContext), ICouplesRepository
{
    public IQueryable<Couple> Get(GetCouplesOptions options)
    {
        var query = QueryableSet;

        if (options.CoupleId is { } coupleId)
        {
            query = query.Where(x => x.Id == coupleId);
        }
        
        if (options.UserId is { } userId)
        {
            query = query.Where(x => x.Memberships.Any(y => y.UserId == userId));
        }
        
        if (options.IncludeMembers)
        {
            query = query.Include(x => x.Memberships);
        }

        if (options.AsNoTracking)
        {
            query = query.AsNoTracking();
        }

        return query;
    }
}