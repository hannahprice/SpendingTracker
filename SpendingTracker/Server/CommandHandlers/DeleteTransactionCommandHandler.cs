using MediatR;
using SpendingTracker.Server.Commands;

namespace SpendingTracker.Server.CommandHandlers;

public class DeleteTransactionCommandHandler : AsyncRequestHandler<DeleteTransactionCommand>
{
    private readonly FinanceContext _dbContext;

    public DeleteTransactionCommandHandler(FinanceContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    protected override async Task Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = await _dbContext.Transactions.FindAsync(new object?[] { request.id }, cancellationToken: cancellationToken);
        if (transaction != null) _dbContext.Transactions.Remove(transaction);
        await _dbContext.SaveChangesAsync(cancellationToken);    
    }
}