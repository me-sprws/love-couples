using CouplesService.Domain.Entities;
using LoveCouples.Domain.Repositories;

namespace CouplesService.Domain.Repositories;

public sealed record GetUsersOptions(
    Guid? UserId = null,
    bool AsNoTracking = false
);

public interface IUsersRepository : IRepository<User>
{
    IQueryable<User> Get(GetUsersOptions options);
}