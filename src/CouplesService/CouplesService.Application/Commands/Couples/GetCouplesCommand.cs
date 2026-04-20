using CouplesService.Application.Contracts.Responses.Couples;
using FluentResults;
using MediatR;

namespace CouplesService.Application.Commands.Couples;

public sealed record GetCouplesCommand(Guid? UserId) : IRequest<Result<List<CoupleResponse>>>;