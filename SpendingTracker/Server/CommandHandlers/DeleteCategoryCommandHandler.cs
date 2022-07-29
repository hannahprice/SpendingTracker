using MediatR;
using SpendingTracker.Server.Commands;

namespace SpendingTracker.Server.CommandHandlers;

public class DeleteCategoryCommandHandler : AsyncRequestHandler<DeleteCategoryCommand>
{
    private readonly FinanceContext _dbContext;

    public DeleteCategoryCommandHandler(FinanceContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    protected override async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _dbContext.Categories.FindAsync(new object?[] { request.id }, cancellationToken: cancellationToken);
        if (category != null) _dbContext.Categories.Remove(category);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}