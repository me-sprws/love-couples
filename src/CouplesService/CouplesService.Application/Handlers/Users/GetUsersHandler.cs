using CouplesService.Application.Commands.Users;
using CouplesService.Application.Common.Mappers;
using CouplesService.Application.Contracts.Responses.Users;
using CouplesService.Domain.Repositories;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CouplesService.Application.Handlers.Users;

public sealed class GetUsersHandler(IUsersRepository repository) 
    : IRequestHandler<GetUsersCommand, Result<List<UserInfoResponse>>>
{
    public async Task<Result<List<UserInfoResponse>>> Handle(GetUsersCommand request, CancellationToken ctk)
    {
        var users = await repository.ToListAsync(
            repository.QueryableSet.AsNoTracking(), ctk);

        return Result.Ok(users.Select(u => u.ToUserInfoResponse()).ToList());
    }
}
