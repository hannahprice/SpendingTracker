using MediatR;
using Microsoft.EntityFrameworkCore;
using SpendingTracker.Server.Queries;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Server.QueryHandlers;

public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, Category>
{
    private readonly FinanceContext _dbContext;

    public GetCategoryQueryHandler(FinanceContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<Category> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        return _dbContext.Categories.Where(x => x.Id == request.id).Include(x => x.Subcategories)
            .FirstAsync(cancellationToken);
    }
}