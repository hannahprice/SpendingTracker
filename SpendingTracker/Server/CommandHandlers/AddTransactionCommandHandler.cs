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

            var category = request.transaction.Category;
            var subCategories = transaction.Subcategories?.ToList() ?? Enumerable.Empty<Subcategory>();

            transaction.Category = null;
            transaction.Subcategories?.Clear();

            _dbContext.Transactions.Attach(transaction);

            _dbContext.Categories.Attach(category!);
            _dbContext.Subcategories.AttachRange(subCategories);

            transaction.Category = null;
            transaction.Subcategories?.Clear();

            transaction.Category = category;
            transaction.Subcategories?.AddRange(subCategories);

            _dbContext.Entry(transaction).State = Microsoft.EntityFrameworkCore.EntityState.Added;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return transaction.Id;
        }
    }
}