namespace CouplesService.Application.Contracts.Requests.Users;

public sealed class UpdateUserInfoRequest
{
    public string Name { get; set; }
    public DateTimeOffset BirthDate { get; set; }
    public string? Country { get; set; }
}