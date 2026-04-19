using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;

namespace CouplesService.WebAPI.Controllers;

[ApiController]
[Route("Auth")]
public sealed class AuthenticationController : ControllerBase
{
    [HttpGet("google")]
    public ActionResult LoginGoogle()
    {
        const string myIdPath = "/id/me";
        
        if (User.Identities.Any(identity =>
                identity is
                {
                    IsAuthenticated: true,
                    AuthenticationType: GoogleDefaults.AuthenticationScheme
                }))
            return Redirect(myIdPath);
        
        return Challenge(new AuthenticationProperties
        {
            RedirectUri = myIdPath
        }, GoogleDefaults.AuthenticationScheme);
    }
    
    [HttpGet("logout")]
    public ActionResult Logout()
    {
        return SignOut();
    }
}