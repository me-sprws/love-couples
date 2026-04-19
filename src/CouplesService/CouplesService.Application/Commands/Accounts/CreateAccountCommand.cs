using CouplesService.Domain.Entities;
using FluentResults;
using MediatR;

namespace CouplesService.Application.Commands.Accounts;

public sealed record CreateAccountCommand(
    string ExternalId,
    string Username,
    Type AccountType,
    string? Email = null
) : IRequest<Result<Account>>;