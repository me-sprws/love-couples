using CouplesService.Application.Contracts.Responses.Couples;
using FluentResults;
using MediatR;

namespace CouplesService.Application.Commands.Couples;

public sealed record CreateCoupleCommand(
    Guid UserId
) : IRequest<Result<CoupleResponse>>;