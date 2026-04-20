using System.Globalization;
using System.Security.Claims;

namespace CouplesService.WebAPI.Extensions;

internal static class UserIdentificationExtensions
{
    public static Guid GetIdentifier(this ClaimsPrincipal user)
    {
        return user.GetValue<Guid>(ClaimTypes.NameIdentifier);
    }
    
    public static TValue GetValue<TValue>(this ClaimsPrincipal user, string claimType)
        where TValue : IParsable<TValue>
    {
        if (user.FindFirstValue(claimType) is not { } valueString)
            throw new InvalidOperationException($"User does not have a claim type '{claimType}'.");

        if (!TValue.TryParse(valueString, CultureInfo.InvariantCulture, out var result))
            throw new FormatException($"Cannot parse claim '{claimType}' value '{valueString}' to {typeof(TValue).Name}.");

        return result;
    }
}