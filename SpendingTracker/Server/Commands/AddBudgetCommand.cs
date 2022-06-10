using MediatR;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Server.Commands
{
    public record AddBudgetCommand(Budget budget) : IRequest<int>;
}
