using CouplesService.Domain.Entities;
using CouplesService.Domain.Repositories;
using LoveCouples.Infrastructure.Persistence.Repositories;

namespace CouplesService.Infrastructure.Persistence.Repositories;

public sealed class UsersRepository(ServiceDbContext dbContext) 
    : Repository<User, ServiceDbContext>(dbContext), IUsersRepository;