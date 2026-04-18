using CouplesService.Application.Contracts.Responses.Users;
using FluentResults;
using MediatR;

namespace CouplesService.Application.Commands.Users;

public sealed record UpdateUserInfoCommand(
    Guid Id, 
    string Name, 
    DateTimeOffset BirthDate, 
    string? Country
) : IRequest<Result<UserInfoResponse>>;