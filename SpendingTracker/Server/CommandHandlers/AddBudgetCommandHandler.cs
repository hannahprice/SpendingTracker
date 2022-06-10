using MediatR;
using SpendingTracker.Server.Commands;

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
            _dbcontext.Budgets.Add(request.budget);
            await _dbcontext.SaveChangesAsync();

            return request.budget.Id;
        }
    }
}
