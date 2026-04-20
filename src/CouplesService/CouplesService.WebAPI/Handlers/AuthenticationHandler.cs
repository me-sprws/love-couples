using System.Security.Claims;
using CouplesService.Application.Commands.Accounts;
using CouplesService.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace CouplesService.WebAPI.Handlers;

internal static class AuthenticationHandler
{
    public static async Task OnCreatingTicket(OAuthCreatingTicketContext context)
    {
        if (context.Identity?.FindFirst(ClaimTypes.NameIdentifier)?.Value is not { } externalId)
        {
            context.Fail("The 'external id' claim is missing.");
            return;
        }
                    
        if (context.Identity?.FindFirst(ClaimTypes.Email)?.Value is not { } email)
        {
            context.Fail("The 'email' claim is missing.");
            return;
        }
                    
        if (context.Identity?.FindFirst(ClaimTypes.Name)?.Value is not { } username)
        {
            context.Fail("The 'username' claim is missing.");
            return;
        }
                    
        var mediator = context.HttpContext.RequestServices
            .GetRequiredService<IMediator>();

        var command = new CreateAccountCommand(
            externalId, 
            username,
            typeof(GoogleAccount),
            email
        );

        var response = await mediator.Send(command);

        if (response.IsFailed)
        {
            context.Fail(response.Errors.FirstOrDefault()?.Message ?? "Unknown error occured.");
            return;
        }

        context.Principal = new(CreateIdentity(response.Value, context.Principal?.Identity?.AuthenticationType));
    }

    static ClaimsIdentity CreateIdentity(Account account, string? authenticationType)
    {
        var identity = new ClaimsIdentity(
        [
            new(ClaimTypes.NameIdentifier, account.User.Id.ToString()),
            new(ClaimTypes.Name, account.User.Name)
        ], authenticationType);

        if (account is EmailAccount emailAccount)
        {
            identity.AddClaim(new(ClaimTypes.Email, emailAccount.Email));
        }

        return identity;
    }
}