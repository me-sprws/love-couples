using System.Security.Claims;
using CouplesService.Application.Contracts.Responses.Identity;
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
        
        var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value is string idString
            ? Guid.Parse(idString)
            : Guid.Empty;

        return Ok(new IdentityInfoResponse
        {
            Id = id,
            Name = name,
            Email = email
        });
    }
}