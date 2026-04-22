using CouplesService.Application.Commands.Couples;
using CouplesService.Domain.Repositories;
using CouplesService.Domain.Services;
using FluentResults;
using LoveCouples.Domain.Services;
using MediatR;

namespace CouplesService.Application.Handlers.Couples;

public sealed class LeaveCoupleHandler(
    ICouplesRepository couplesRepository,
    IDateTimeProvider dateTimeProvider
) : IRequestHandler<LeaveCoupleCommand, Result>
{
    public async Task<Result> Handle(LeaveCoupleCommand request, CancellationToken ctk)
    {
        var couple = await couplesRepository.FirstOrDefaultAsync(
            couplesRepository.Get(new(
                CoupleId: request.CoupleId,
                UserId: request.LeaverUserId,
                IncludeMembers: true)), 
            ctk);

        if (couple is null)
            return Result.Fail("Couple not found.");

        if (couple.Separate(x => x.UserId == request.LeaverUserId, dateTimeProvider) is
            {
                IsFailed: true
            } separate)
            return separate;
        
        await couplesRepository.UnitOfWork.SaveChangesAsync(ctk);
        
        return Result.Ok();
    }
}