using FluentResults;
using MediatR;

namespace CouplesService.Application.Commands.Couples;

public sealed record LeaveCoupleCommand(
    Guid CoupleId,
    Guid LeaverUserId
) : IRequest<Result>;