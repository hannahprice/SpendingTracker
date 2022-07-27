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
        var budget = await _dbContext.Budgets.FindAsync(new object?[] { request.id }, cancellationToken: cancellationToken);
        if (budget != null) _dbContext.Budgets.Remove(budget);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}