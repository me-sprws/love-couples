using CouplesService.Domain.Entities;
using CouplesService.Domain.Repositories;
using LoveCouples.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CouplesService.Infrastructure.Persistence.Repositories;

public sealed class UsersRepository(ServiceDbContext dbContext) 
    : Repository<User, ServiceDbContext>(dbContext), IUsersRepository
{
    public IQueryable<User> Get(GetUsersOptions options)
    {
        var query = QueryableSet;
        
        if (options.UserId is { } userId)
        {
            query = query.Where(x => x.Id == userId);
        }

        if (options.AsNoTracking)
        {
            query = query.AsNoTracking();
        }

        return query;
    }
}