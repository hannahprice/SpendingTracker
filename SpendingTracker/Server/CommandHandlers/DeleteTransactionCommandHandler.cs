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
        _dbContext.Transactions.Remove(_dbContext.Transactions.Find(request.id));
        await _dbContext.SaveChangesAsync(cancellationToken);    
    }
}