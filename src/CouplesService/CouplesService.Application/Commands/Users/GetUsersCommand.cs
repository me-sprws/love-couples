using CouplesService.Application.Contracts.Responses.Users;
using FluentResults;
using MediatR;

namespace CouplesService.Application.Commands.Users;

public sealed record GetUsersCommand : IRequest<Result<List<UserInfoResponse>>>;