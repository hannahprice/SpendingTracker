using MediatR;
using Microsoft.EntityFrameworkCore;
using SpendingTracker.Server.Queries;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Server.QueryHandlers
{
    public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, List<Transaction>>
    {
        private readonly FinanceContext _dbcontext;

        public GetTransactionsQueryHandler(FinanceContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public Task<List<Transaction>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            return _dbcontext.Transactions
                .OrderByDescending(x => x.DateOfTransaction)
                .Include(x => x.Categories)
                .Include(x => x.Subcategories)
                .ToListAsync();
        }
    }
}