using CouplesService.Application.Commands;
using CouplesService.Application.Contracts.Responses.Users;
using CouplesService.Domain.Repositories;
using MediatR;

namespace CouplesService.Application.Handlers.Users;

public sealed class UpdateUserInfoHandler(IUsersRepository repository) 
    : IRequestHandler<UpdateUserInfoCommand, UserInfoResponse>
{
    public async Task<UserInfoResponse> Handle(UpdateUserInfoCommand request, CancellationToken ctk)
    {
        var user = await repository.FirstOrDefaultAsync(repository.QueryableSet, request.Id, ctk);

        if (user is null)
        {
            throw new("User not found"); // TODO: API Exceptions with codes
        }
        
        user.UpdateInfo(request.Name, request.Country, request.BirthDate);

        await repository.UnitOfWork.SaveChangesAsync(ctk);

        return new()
        {
            Id = user.Id,
            Name = user.Name,
            Country = user.Country,
            BirthDate = user.BirthDate
        };
    }
}