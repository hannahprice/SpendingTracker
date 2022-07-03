using MediatR;
using SpendingTracker.Server.Commands;

namespace SpendingTracker.Server.CommandHandlers
{
    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, int>
    {
        private readonly FinanceContext _dbContext;

        public AddCategoryCommandHandler(FinanceContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            _dbContext.Categories.Add(request.category);
            await _dbContext.SaveChangesAsync();

            return request.category.Id;
        }
    }
}