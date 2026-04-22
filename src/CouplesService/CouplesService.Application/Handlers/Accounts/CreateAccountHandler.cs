using CouplesService.Application.Commands.Accounts;
using CouplesService.Application.Commands.Users;
using CouplesService.Domain.Entities;
using CouplesService.Domain.Repositories;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CouplesService.Application.Handlers.Accounts;

public sealed class CreateAccountHandler(IMediator mediator, IAccountsRepository repository)
    : IRequestHandler<CreateAccountCommand, Result<Account>>
{
    public Task<Result<Account>> Handle(CreateAccountCommand request, CancellationToken ctk)
    {
        if (request.AccountType == typeof(GoogleAccount))
        {
            if (string.IsNullOrWhiteSpace(request.Email))
                return Task.FromResult(Result.Fail<Account>("The email is required to create a google account."));
            
            return CreateGoogle(request.ExternalId, request.Username, request.Email, ctk);
        }

        return Task.FromResult(Result.Fail<Account>("Unavailable account type."));
    }

    async Task<Result<Account>> CreateGoogle(string externalId, string username, string email, CancellationToken ctk)
    {
        var google = await repository.FirstOrDefaultAsync(
            repository.QueryableSet
                .Include(x => x.User)
                .Where(x => x.ExternalId == externalId)
                .AsNoTracking(), 
            ctk);

        if (google is not null)
        {
            return Result.Ok(google);
        }

        var userResult = await CreateUser(username, ctk);

        if (userResult.IsFailed)
        {
            return Result.Fail(userResult.Errors);
        }
        
        google = new GoogleAccount
        {
            Email = email,
            Username = username,
            ExternalId =  externalId,
            User = userResult.Value
        };
        
        await repository.AddAsync(google, ctk);
        await repository.UnitOfWork.SaveChangesAsync(ctk);
        
        return Result.Ok(google);
    }

    Task<Result<User>> CreateUser(string username, CancellationToken ctk)
    {
        var command = new CreateUserCommand(username, null, null);
        return mediator.Send(command, ctk);
    }
}