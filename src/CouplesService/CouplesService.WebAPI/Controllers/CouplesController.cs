using System.Security.Claims;
using CouplesService.Application.Commands.Couples;
using CouplesService.Application.Contracts.Responses.Couples;
using CouplesService.Domain.ValueObjects;
using CouplesService.WebAPI.Extensions;
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
        var userId = User.GetValue<Guid>(ClaimTypes.NameIdentifier);
        
        var response = await mediator.Send(
            new CreateCoupleCommand(
                userId,
                CouplesStatus.Alone,
                null
            )
        );

        return response.ToActionResult();
    }

    [HttpGet]
    public async Task<ActionResult<List<CoupleResponse>>> GetCouples()
    {
        var response = await mediator.Send(
            new GetCouplesCommand());
        
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