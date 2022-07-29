using MediatR;
using SpendingTracker.Server.Commands;

namespace SpendingTracker.Server.CommandHandlers;

public class DeleteSubcategoryCommandHandler : AsyncRequestHandler<DeleteSubcategoryCommand>
{
    private readonly FinanceContext _dbContext;

    public DeleteSubcategoryCommandHandler(FinanceContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    protected override async Task Handle(DeleteSubcategoryCommand request, CancellationToken cancellationToken)
    {
        var subcategory = await _dbContext.Subcategories.FindAsync(new object?[] { request.id }, cancellationToken: cancellationToken);
        if (subcategory != null) _dbContext.Subcategories.Remove(subcategory);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}