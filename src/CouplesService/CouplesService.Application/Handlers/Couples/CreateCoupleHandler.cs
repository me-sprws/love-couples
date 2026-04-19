using CouplesService.Application.Commands.Couples;
using CouplesService.Application.Common.Mappers;
using CouplesService.Application.Contracts.Responses.Couples;
using CouplesService.Domain.Entities;
using CouplesService.Domain.Repositories;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CouplesService.Application.Handlers.Couples;

public sealed class CreateCoupleHandler(
    IUsersRepository usersRepository, 
    ICouplesRepository couplesRepository
) : IRequestHandler<CreateCoupleCommand, Result<CoupleResponse>>
{
    public async Task<Result<CoupleResponse>> Handle(CreateCoupleCommand request, CancellationToken ctk)
    {
        var userExists = await usersRepository.AnyAsync(
            usersRepository.QueryableSet
                .Where(u => u.Id == request.UserId)
                .AsNoTracking(), 
            ctk);

        if (!userExists)
        {
            return Result.Fail("User not exists");
        }
        
        var couple = new Couple
        {
            Status = request.Status,
            TogetherSince = request.TogetherAt,
            Memberships = [
                new()
                {
                    UserId = request.UserId,
                }
            ]
        };

        await couplesRepository.AddAsync(couple, ctk);
        await couplesRepository.UnitOfWork.SaveChangesAsync(ctk);

        return Result.Ok(couple.ToCoupleResponse());
    }
}