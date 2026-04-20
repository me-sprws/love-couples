using CouplesService.Application.Commands.Couples;
using CouplesService.Domain.Repositories;
using CouplesService.Domain.ValueObjects;
using FluentResults;
using MediatR;

namespace CouplesService.Application.Handlers.Couples;

public sealed class LeaveCoupleHandler(ICouplesRepository couplesRepository)
    : IRequestHandler<LeaveCoupleCommand, Result>
{
    public async Task<Result> Handle(LeaveCoupleCommand request, CancellationToken ctk)
    {
        var couple = await couplesRepository.FirstOrDefaultAsync(
            couplesRepository.Get(new(
                CoupleId: request.CoupleId,
                UserId: request.LeaverUserId,
                IncludeMembers: true)), 
            ctk);
        
        var membership = couple?.Memberships.FirstOrDefault(x => x.UserId == request.LeaverUserId);
        
        if (membership is null || couple is null)
        {
            return Result.Fail("The user is not in this couples.");
        }

        couple.Memberships.Remove(membership);
        couple.Status = CouplesStatus.Separated;
        couple.SeparatedAt = DateTimeOffset.UtcNow;

        await couplesRepository.UnitOfWork.SaveChangesAsync(ctk);
        
        return Result.Ok();
    }
}