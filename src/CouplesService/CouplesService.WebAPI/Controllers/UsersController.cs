using CouplesService.Application.Commands.Users;
using CouplesService.Application.Contracts.Requests.Users;
using CouplesService.Application.Contracts.Responses.Users;
using FluentResults.Extensions.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CouplesService.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class UsersController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserInfoResponse>>> GetUsers()
    {
        var response = await mediator.Send(new GetUsersCommand());
        return response.ToActionResult();
    }
    
    [HttpPut]
    public async Task<ActionResult<UserInfoResponse>> UpdateUserInfo(UpdateUserInfoRequest request)
    {
        var command = new UpdateUserInfoCommand(
            Guid.NewGuid(), 
            request.Name, 
            request.BirthDate, 
            request.Country
        );
        
        var response = await mediator.Send(command);

        return response.ToActionResult();
    }
}