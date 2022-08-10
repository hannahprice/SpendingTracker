using MediatR;
using Microsoft.EntityFrameworkCore;
using SpendingTracker.Server.Queries;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Server.QueryHandlers;

public class GetTransactionQueryHandler : IRequestHandler<GetTransactionQuery, Transaction>
{
    private readonly FinanceContext _dbContext;

    public GetTransactionQueryHandler(FinanceContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Transaction> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
    {
        return _dbContext.Transactions.Where(x => x.Id == request.id)
            .Include(x => x.Category)
            .Include(x => x.Subcategories)
            .FirstAsync(cancellationToken: cancellationToken);
    }
}