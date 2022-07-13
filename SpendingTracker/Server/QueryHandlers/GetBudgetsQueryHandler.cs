using MediatR;
using Microsoft.EntityFrameworkCore;
using SpendingTracker.Server.Queries;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Server.QueryHandlers
{
    public class GetBudgetsQueryHandler : IRequestHandler<GetBudgetsQuery, List<Budget>>
    {
        private readonly FinanceContext _dbContext;

        public GetBudgetsQueryHandler(FinanceContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Budget>> Handle(GetBudgetsQuery request, CancellationToken cancellationToken)
        {
            return _dbContext.Budgets
                .OrderByDescending(x => x.Id)
                .Include(x => x.Categories)
                .Include(x => x.Subcategories)
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}