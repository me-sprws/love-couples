using CouplesService.Application.Contracts.Responses.Users;
using CouplesService.Domain.Entities;

namespace CouplesService.Application.Common.Mappers;

public static class UserMappingExtensions
{
    public static UserInfoResponse ToUserInfoResponse(this User user) =>
        new()
        {
            Id = user.Id,
            Name = user.Name,
            BirthDate = user.BirthDate,
            Country = user.Country,
        };
}