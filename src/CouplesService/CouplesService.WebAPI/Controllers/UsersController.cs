using CouplesService.Application.Commands;
using CouplesService.Application.Contracts.Requests.Users;
using CouplesService.Application.Contracts.Responses.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CouplesService.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public sealed class UsersController(IMediator mediator) : ControllerBase
{
    [HttpPut]
    public async Task<ActionResult<UserInfoResponse>> UpdateUserInfo(UpdateUserInfoRequest request)
    {
        var command = new UpdateUserInfoCommand
        {
            Name = request.Name,
            BirthDate = request.BirthDate,
            Country = request.Country
        };
        
        var response = await mediator.Send(command);
        
        return Ok(response);
    }
}