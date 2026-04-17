using CouplesService.Application.Contracts.Responses.Users;
using MediatR;

namespace CouplesService.Application.Commands;

public sealed class UpdateUserInfoCommand : IRequest<UserInfoResponse>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTimeOffset BirthDate { get; set; }
    public string? Country { get; set; }
}