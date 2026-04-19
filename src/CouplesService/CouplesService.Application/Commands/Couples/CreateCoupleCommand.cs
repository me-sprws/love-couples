using CouplesService.Application.Contracts.Responses.Couples;
using CouplesService.Domain.ValueObjects;
using FluentResults;
using MediatR;

namespace CouplesService.Application.Commands.Couples;

public sealed record CreateCoupleCommand(
    Guid UserId,
    CouplesStatus Status,
    DateTimeOffset TogetherAt
) : IRequest<Result<CoupleResponse>>;