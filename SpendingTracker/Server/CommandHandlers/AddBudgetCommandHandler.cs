using MediatR;
using SpendingTracker.Server.Commands;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Server.CommandHandlers
{
    public class AddBudgetCommandHandler : IRequestHandler<AddBudgetCommand, int>
    {
        private readonly FinanceContext _dbcontext;

        public AddBudgetCommandHandler(FinanceContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<int> Handle(AddBudgetCommand request, CancellationToken cancellationToken)
        {
            var budget = request.budget;

            var categories = request.budget.Categories.ToList();
            var subCategories = budget.Subcategories != null ? budget.Subcategories.ToList() : Enumerable.Empty<Subcategory>();

            budget.Categories.Clear();
            budget.Subcategories?.Clear();

            _dbcontext.Budgets.Attach(budget);
            
            foreach (Category category in categories) { _dbcontext.Categories.Attach(category); }
            foreach (Subcategory subCategory in subCategories) { _dbcontext.Subcategories.Attach(subCategory); }

            budget.Categories.Clear();
            budget.Subcategories?.Clear();

            foreach (Category category in categories) { budget.Categories.Add(category); }
            foreach (Subcategory subCategory in subCategories) { budget.Subcategories.Add(subCategory); }

            _dbcontext.Entry(budget).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            
            await _dbcontext.SaveChangesAsync();

            return request.budget.Id;
        }
    }
}
