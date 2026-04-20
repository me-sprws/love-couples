using System.Security.Claims;
using CouplesService.Application.Contracts.Responses.Identity;
using CouplesService.WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CouplesService.WebAPI.Controllers;

[ApiController]
[Route("Id")]
public sealed class IdentityController : ControllerBase
{
    [Authorize]
    [HttpGet("me")]
    public ActionResult<IdentityInfoResponse> GetIdentity()
    {
        var name = User.Identity?.Name;
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        
        return Ok(new IdentityInfoResponse
        {
            Id = User.GetIdentifier(),
            Name = name,
            Email = email
        });
    }
}