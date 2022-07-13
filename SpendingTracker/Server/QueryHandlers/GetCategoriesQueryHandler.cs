using MediatR;
using Microsoft.EntityFrameworkCore;
using SpendingTracker.Server.Queries;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Server.QueryHandlers
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<Category>>
    {
        private readonly FinanceContext _dbContext;

        public GetCategoriesQueryHandler(FinanceContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Category>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            return _dbContext.Categories
                .OrderByDescending(x => x.Id)
                .Include(x => x.Subcategories)
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}