using MediatR;
using SpendingTracker.Server.Commands;

namespace SpendingTracker.Server.CommandHandlers
{
    public class AddSubcategoryCommandHandler : IRequestHandler<AddSubcategoryCommand, int>
    {
        private readonly FinanceContext _dbContext;

        public AddSubcategoryCommandHandler(FinanceContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(AddSubcategoryCommand request, CancellationToken cancellationToken)
        {
            _dbContext.Subcategories.Add(request.subcategory);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return request.subcategory.Id;
        }
    }
}