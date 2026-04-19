namespace CouplesService.Application.Contracts.Responses.Identity;

public sealed class IdentityInfoResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
}