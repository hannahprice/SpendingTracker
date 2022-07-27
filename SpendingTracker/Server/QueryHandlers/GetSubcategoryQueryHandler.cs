using MediatR;
using Microsoft.EntityFrameworkCore;
using SpendingTracker.Server.Queries;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Server.QueryHandlers;

public class GetSubcategoryQueryHandler : IRequestHandler<GetSubcategoryQuery, Subcategory>
{
    private readonly FinanceContext _dbContext;

    public GetSubcategoryQueryHandler(FinanceContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<Subcategory> Handle(GetSubcategoryQuery request, CancellationToken cancellationToken)
    {
        return _dbContext.Subcategories.Where(x => x.Id == request.id).FirstAsync(cancellationToken);
    }
}