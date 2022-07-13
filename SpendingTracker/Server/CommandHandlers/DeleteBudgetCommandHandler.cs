using MediatR;
using SpendingTracker.Server.Commands;

namespace SpendingTracker.Server.CommandHandlers;

public class DeleteBudgetCommandHandler : AsyncRequestHandler<DeleteBudgetCommand>
{
    private readonly FinanceContext _dbContext;

    public DeleteBudgetCommandHandler(FinanceContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    protected override async Task Handle(DeleteBudgetCommand request, CancellationToken cancellationToken)
    {
        _dbContext.Budgets.Remove(_dbContext.Budgets.Find(request.id));
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}