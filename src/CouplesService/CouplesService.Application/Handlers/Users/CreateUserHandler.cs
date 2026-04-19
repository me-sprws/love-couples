using CouplesService.Application.Commands.Users;
using CouplesService.Domain.Entities;
using CouplesService.Domain.Repositories;
using FluentResults;
using MediatR;

namespace CouplesService.Application.Handlers.Users;

public sealed class CreateUserHandler(IUsersRepository repository)
    : IRequestHandler<CreateUserCommand, Result<User>>
{
    public async Task<Result<User>> Handle(CreateUserCommand request, CancellationToken ctk)
    {
        var user = new User
        {
            Name = request.Name,
            Country = request.Country,
            BirthDate = request.DateOfBirth ?? DateTimeOffset.Now // TODO: Birth date is not required
        };
        
        await repository.AddAsync(user, ctk);
        await repository.UnitOfWork.SaveChangesAsync(ctk);
        
        return  Result.Ok(user);
    }
}