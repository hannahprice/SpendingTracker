using MediatR;
using Microsoft.EntityFrameworkCore;
using SpendingTracker.Server.Queries;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Server.QueryHandlers
{
    public class GetBudgetQueryHandler : IRequestHandler<GetBudgetQuery, Budget>
    {
        private readonly FinanceContext _dbContext;

        public GetBudgetQueryHandler(FinanceContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Budget> Handle(GetBudgetQuery request, CancellationToken cancellationToken)
        {
            return _dbContext.Budgets.Where(x => x.Id == request.id)
                .Include(x => x.Categories)
                .Include(x => x.Subcategories).FirstAsync(cancellationToken: cancellationToken);
        }
    }
}