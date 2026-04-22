using CouplesService.Domain.Entities;

namespace CouplesService.Domain.Tests.Builders;

internal static class MembershipBuilder
{
    public static Membership Build() => new() { User = new() };
}