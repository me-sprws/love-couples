using CouplesService.Application.Commands.Couples;
using CouplesService.Application.Common.Mappers;
using CouplesService.Application.Contracts.Responses.Couples;
using CouplesService.Domain.Entities;
using CouplesService.Domain.Repositories;
using FluentResults;
using LoveCouples.Domain.Services;
using MediatR;

namespace CouplesService.Application.Handlers.Couples;

public sealed class CreateCoupleHandler(
    IUsersRepository usersRepository, 
    ICouplesRepository couplesRepository,
    IDateTimeProvider dateTimeProvider
) : IRequestHandler<CreateCoupleCommand, Result<CoupleResponse>>
{
    public async Task<Result<CoupleResponse>> Handle(CreateCoupleCommand request, CancellationToken ctk)
    {
        if (!await usersRepository.ExistsAsync(request.UserId, ctk))
            return Result.Fail("User not exists.");

        var couple = Couple.Create(request.UserId, dateTimeProvider);
        
        await couplesRepository.AddAsync(couple, ctk);
        await couplesRepository.UnitOfWork.SaveChangesAsync(ctk);

        return Result.Ok(couple.ToCoupleResponse());
    }
}