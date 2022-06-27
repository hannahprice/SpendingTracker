using MediatR;
using Microsoft.EntityFrameworkCore;
using SpendingTracker.Server.Queries;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Server.QueryHandlers
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<Category>>
    {
        private readonly FinanceContext _dbcontext;

        public GetCategoriesQueryHandler(FinanceContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public Task<List<Category>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            return _dbcontext.Categories
                .Include(x => x.Subcategories)
                .ToListAsync();
        }
    }
}
