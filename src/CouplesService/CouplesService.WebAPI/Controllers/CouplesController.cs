using System.Security.Claims;
using CouplesService.Application.Commands.Couples;
using CouplesService.Application.Contracts.Responses.Couples;
using CouplesService.Domain.ValueObjects;
using FluentResults.Extensions.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CouplesService.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public sealed class CouplesController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CoupleResponse>> CreateCouple()
    {
        if (User.FindFirstValue(ClaimTypes.NameIdentifier) is not {} userIdString)
            return Unauthorized();
        
        var userId = Guid.Parse(userIdString);
        
        var command = new CreateCoupleCommand(
            userId,
            CouplesStatus.Alone,
            DateTimeOffset.UtcNow
        );

        var response = await mediator.Send(command);

        return response.ToActionResult();
    }

    [HttpGet]
    public async Task<ActionResult<List<CoupleResponse>>> GetCouples()
    {
        var command = new GetCouplesCommand();
        
        var response = await mediator.Send(command);
        
        return response.ToActionResult();
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