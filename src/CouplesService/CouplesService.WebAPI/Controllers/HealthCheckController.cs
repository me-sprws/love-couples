using Microsoft.AspNetCore.Mvc;

namespace CouplesService.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class HealthCheckController : ControllerBase
{
    [HttpGet]
    public OkResult Check()
    {
        return Ok();
    }
}