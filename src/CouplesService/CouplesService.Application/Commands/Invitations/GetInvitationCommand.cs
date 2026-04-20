using CouplesService.Application.Contracts.Responses.Invitations;
using FluentResults;
using MediatR;

namespace CouplesService.Application.Commands.Invitations;

public sealed record GetInvitationCommand(
    Guid CoupleId,
    Guid SenderId
) : IRequest<Result<InvitationResponse>>;
