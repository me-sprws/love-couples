using System.Security.Claims;
using CouplesService.Application.Commands.Users;
using CouplesService.Application.Contracts.Requests.Users;
using CouplesService.Application.Contracts.Responses.Identity;
using CouplesService.WebAPI.Extensions;
using FluentResults.Extensions.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CouplesService.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("Id")]
public sealed class IdentityController(IMediator mediator) : ControllerBase
{
    [HttpGet("Me")]
    public ActionResult<IdentityInfoResponse> GetIdentity()
    {
        return Ok(new IdentityInfoResponse
        {
            Id = User.GetIdentifier(),
            Name = User.Identity?.Name,
            Email = User.FindFirst(ClaimTypes.Email)?.Value
        });
    }
    
    [HttpPut("Info")]
    public async Task<ActionResult<IdentityInfoResponse>> UpdateUserInfo(UpdateUserInfoRequest request)
    {
        var command = new UpdateUserInfoCommand(
            User.GetIdentifier(),
            request.Name,
            request.BirthDate,
            request.Country
        );
        
        var response = await mediator.Send(command);

        return response.ToActionResult();
    }
}