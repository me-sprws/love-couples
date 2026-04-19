using System.Security.Claims;
using CouplesService.Application.Contracts.Responses.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CouplesService.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
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
            Name = name,
            Email = email
        });
    }
    
    [HttpGet("login")]
    public ActionResult Login()
    {
        return Challenge(new AuthenticationProperties
        {
            RedirectUri = "/identity/me"
        }, GoogleDefaults.AuthenticationScheme);
    }
    
    [HttpGet("logout")]
    public ActionResult Logout()
    {
        return SignOut();
    }
}