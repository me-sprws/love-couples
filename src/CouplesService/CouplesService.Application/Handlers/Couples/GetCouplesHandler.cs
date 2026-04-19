using CouplesService.Application.Commands.Couples;
using CouplesService.Application.Common.Mappers;
using CouplesService.Application.Contracts.Responses.Couples;
using CouplesService.Domain.Repositories;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CouplesService.Application.Handlers.Couples;

public sealed class GetCouplesHandler(ICouplesRepository repository) 
    : IRequestHandler<GetCouplesCommand, Result<List<CoupleResponse>>>
{
    public async Task<Result<List<CoupleResponse>>> Handle(GetCouplesCommand request, CancellationToken ctk)
    {
        var couples = await repository.ToListAsync(
            repository.QueryableSet
                .Include(x => x.Memberships)
                .AsNoTracking(), 
            ctk);
        
        return Result.Ok(couples.Select(c => c.ToCoupleResponse()).ToList());
    }
}