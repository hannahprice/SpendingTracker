using MediatR;
using Microsoft.EntityFrameworkCore;
using SpendingTracker.Server.Queries;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Server.QueryHandlers
{
    public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, List<Transaction>>
    {
        private readonly FinanceContext _financeContext;

        public GetTransactionsQueryHandler(FinanceContext financeContext)
        {
            _financeContext = financeContext;
        }

        public Task<List<Transaction>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            return _financeContext.Transactions
                .Include(x => x.Categories)
                .Include(x => x.Subcategories)
                .ToListAsync();
        }
    }
}
