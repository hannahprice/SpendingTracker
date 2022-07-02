using MediatR;
using SpendingTracker.Server.Commands;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Server.CommandHandlers
{
    public class AddTransactionCommandHandler : IRequestHandler<AddTransactionCommand, int>
    {
        private readonly FinanceContext _dbContext;

        public AddTransactionCommandHandler(FinanceContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(AddTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = request.transaction;

            var categories = transaction.Categories != null ? transaction.Categories.ToList() : Enumerable.Empty<Category>();
            var subCategories = transaction.Subcategories != null ? transaction.Subcategories.ToList() : Enumerable.Empty<Subcategory>();

            transaction.Categories?.Clear();
            transaction.Subcategories?.Clear();

            _dbContext.Transactions.Attach(transaction);

            _dbContext.Categories.AttachRange(categories);
            _dbContext.Subcategories.AttachRange(subCategories);

            transaction.Categories?.Clear();
            transaction.Subcategories?.Clear();

            transaction.Categories?.AddRange(categories);
            transaction.Subcategories?.AddRange(subCategories);

            _dbContext.Entry(transaction).State = Microsoft.EntityFrameworkCore.EntityState.Added;

            await _dbContext.SaveChangesAsync();

            return transaction.Id;
        }
    }
}