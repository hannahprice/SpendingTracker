using MediatR;
using SpendingTracker.Server.Commands;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Server.CommandHandlers
{
    public class AddBudgetCommandHandler : IRequestHandler<AddBudgetCommand, int>
    {
        private readonly FinanceContext _dbContext;

        public AddBudgetCommandHandler(FinanceContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(AddBudgetCommand request, CancellationToken cancellationToken)
        {
            var budget = request.budget;

            var category = request.budget.Category;
            var subCategories = budget.Subcategories?.ToList() ?? Enumerable.Empty<Subcategory>();

            budget.Category = null;
            budget.Subcategories?.Clear();

            _dbContext.Budgets.Attach(budget);

            _dbContext.Categories.Attach(category);
            _dbContext.Subcategories.AttachRange(subCategories);

            budget.Category = null;
            budget.Subcategories?.Clear();

            budget.Category = category;
            budget.Subcategories?.AddRange(subCategories);

            _dbContext.Entry(budget).State = Microsoft.EntityFrameworkCore.EntityState.Added;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return request.budget.Id;
        }
    }
}