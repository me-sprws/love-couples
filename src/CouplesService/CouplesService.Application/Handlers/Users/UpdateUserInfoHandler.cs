using CouplesService.Application.Commands.Users;
using CouplesService.Application.Common.Mappers;
using CouplesService.Application.Contracts.Responses.Users;
using CouplesService.Domain.Repositories;
using FluentResults;
using MediatR;

namespace CouplesService.Application.Handlers.Users;

public sealed class UpdateUserInfoHandler(IUsersRepository repository) 
    : IRequestHandler<UpdateUserInfoCommand, Result<UserInfoResponse>>
{
    public async Task<Result<UserInfoResponse>> Handle(UpdateUserInfoCommand request, CancellationToken ctk)
    {
        var user = await repository.FirstOrDefaultAsync(repository.QueryableSet, request.Id, ctk);

        if (user is null)
        {
            return Result.Fail<UserInfoResponse>("User not found.");
        }
        
        user.UpdateInfo(request.Name, request.Country, request.BirthDate);

        await repository.UnitOfWork.SaveChangesAsync(ctk);

        return Result.Ok(user.ToUserInfoResponse());
    }
}