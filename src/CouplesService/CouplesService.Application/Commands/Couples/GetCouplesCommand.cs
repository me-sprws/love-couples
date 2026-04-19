using CouplesService.Application.Contracts.Responses.Couples;
using FluentResults;
using MediatR;

namespace CouplesService.Application.Commands.Couples;

public sealed record GetCouplesCommand : IRequest<Result<List<CoupleResponse>>>;