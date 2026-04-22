using CouplesService.Application.Commands.Couples;
using CouplesService.Application.Commands.Invitations;
using CouplesService.Application.Contracts.Responses.Couples;
using CouplesService.Application.Contracts.Responses.Invitations;
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
        var response = await mediator.Send(
            new CreateCoupleCommand(User.GetIdentifier())
        );

        return response.ToActionResult();
    }
    
    [HttpDelete("{coupleId:guid}")]
    public async Task<ActionResult<CoupleResponse>> LeaveCouple(Guid coupleId)
    {
        var response = await mediator.Send(
            new LeaveCoupleCommand(
                coupleId,
                User.GetIdentifier()
            )
        );

        return response.ToActionResult();
    }

    [HttpGet]
    public async Task<ActionResult<List<CoupleResponse>>> GetCouples()
    {
        var response = await mediator.Send(
            new GetCouplesCommand(User.GetIdentifier()));
        
        return response.ToActionResult();
    }
    
    [HttpPut("{coupleId:guid}")]
    public Task UpdateCoupleInfo(Guid coupleId)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet("{coupleId:guid}/Invite")]
    public async Task<ActionResult<InvitationResponse>> GetCoupleInvitation(Guid coupleId)
    {
        var command = new GetInvitationCommand(coupleId, User.GetIdentifier());
        
        var response = await mediator.Send(command);
        
        return response.ToActionResult();
    }
    
    [HttpPost("Invite/{code}")]
    public async Task<ActionResult> AcceptCoupleInvitation(string code)
    {
        var command = new AcceptInvitationCommand(code, User.GetIdentifier());

        var response = await mediator.Send(command);
        
        return response.ToActionResult();
    }
}