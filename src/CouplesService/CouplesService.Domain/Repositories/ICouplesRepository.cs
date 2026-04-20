using CouplesService.Domain.Entities;
using LoveCouples.Domain.Repositories;

namespace CouplesService.Domain.Repositories;

public sealed record GetCouplesOptions(
    Guid? CoupleId = null,
    Guid? UserId = null,
    bool AsNoTracking = false
);

public interface ICouplesRepository : IRepository<Couple>
{
    IQueryable<Couple> Get(GetCouplesOptions options);
}