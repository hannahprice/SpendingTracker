using MediatR;
using Microsoft.EntityFrameworkCore;
using SpendingTracker.Server.Queries;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Server.QueryHandlers
{
    public class GetBudgetsQueryHandler : IRequestHandler<GetBudgetsQuery, List<Budget>>
    {
        private readonly FinanceContext _dbcontext;

        public GetBudgetsQueryHandler(FinanceContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public Task<List<Budget>> Handle(GetBudgetsQuery request, CancellationToken cancellationToken)
        {
            return _dbcontext.Budgets
                .OrderByDescending(x => x.Id)
                .Include(x => x.Categories)
                .Include(x => x.Subcategories)
                .ToListAsync();
        }
    }
}