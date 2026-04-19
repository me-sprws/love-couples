using CouplesService.Domain.Entities;
using CouplesService.Domain.Repositories;
using LoveCouples.Infrastructure.Persistence.Repositories;

namespace CouplesService.Infrastructure.Persistence.Repositories;

public sealed class AccountsRepository(ServiceDbContext dbContext) 
    : Repository<Account, ServiceDbContext>(dbContext), IAccountsRepository;