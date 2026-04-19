using CouplesService.Domain.Entities;
using FluentResults;
using MediatR;

namespace CouplesService.Application.Commands.Users;

public sealed record CreateUserCommand(
    string Name,
    string? Country,
    DateTimeOffset? DateOfBirth
) : IRequest<Result<User>>;