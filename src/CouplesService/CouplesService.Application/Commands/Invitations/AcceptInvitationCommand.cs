using FluentResults;
using MediatR;

namespace CouplesService.Application.Commands.Invitations;

public sealed record AcceptInvitationCommand(
    string InvitationCode,
    Guid InviteeUserId
) : IRequest<Result>;