using MediatR;
using Microsoft.EntityFrameworkCore;
using SpendingTracker.Server.Queries;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Server.QueryHandlers
{
    public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, List<Transaction>>
    {
        private readonly FinanceContext _dbContext;

        public GetTransactionsQueryHandler(FinanceContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Transaction>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            return _dbContext.Transactions
                .OrderByDescending(x => x.DateOfTransaction)
                .Include(x => x.Category)
                .Include(x => x.Subcategories)
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}