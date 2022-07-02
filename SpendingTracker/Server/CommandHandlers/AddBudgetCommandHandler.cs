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

            var categories = request.budget.Categories.ToList();
            var subCategories = budget.Subcategories != null ? budget.Subcategories.ToList() : Enumerable.Empty<Subcategory>();

            budget.Categories.Clear();
            budget.Subcategories?.Clear();

            _dbContext.Budgets.Attach(budget);

            foreach (Category category in categories) { _dbContext.Categories.Attach(category); }
            foreach (Subcategory subCategory in subCategories) { _dbContext.Subcategories.Attach(subCategory); }

            budget.Categories.Clear();
            budget.Subcategories?.Clear();

            foreach (Category category in categories) { budget.Categories.Add(category); }
            foreach (Subcategory subCategory in subCategories) { budget.Subcategories.Add(subCategory); }

            _dbContext.Entry(budget).State = Microsoft.EntityFrameworkCore.EntityState.Added;

            await _dbContext.SaveChangesAsync();

            return request.budget.Id;
        }
    }
}