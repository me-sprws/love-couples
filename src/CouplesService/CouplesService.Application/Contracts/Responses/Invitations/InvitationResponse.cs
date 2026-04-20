namespace CouplesService.Application.Contracts.Responses.Invitations;

public sealed class InvitationResponse
{
    public Guid CoupleId { get; set; }
    public string Code { get; set; }
}