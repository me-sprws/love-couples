namespace CouplesService.Application.Contracts.Responses.Users;

public sealed class UserInfoResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTimeOffset? BirthDate { get; set; }
    public string? Country { get; set; }
}