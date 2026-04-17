using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CouplesService.WebAPI.Controllers;

[Authorize]
[ApiController]
public sealed class CouplesController : ControllerBase
{
    [HttpPost]
    public Task CreateCouple()
    {
        return Task.CompletedTask;
    }
    
    [HttpPut("{coupleId:guid}")]
    public Task UpdateCoupleInfo(Guid coupleId)
    {
        return Task.CompletedTask;
    }
    
    [HttpGet("{coupleId:guid}/invite")]
    public Task GetCoupleInvitation(Guid coupleId)
    {
        return Task.CompletedTask;
    }
    
    [HttpPost("{coupleId:guid}/invite/{code}")]
    public Task AcceptCoupleInvitation(Guid coupleId, string code)
    {
        return Task.CompletedTask;
    }
}