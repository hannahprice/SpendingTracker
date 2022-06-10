using MediatR;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Server.Queries
{
    public record GetBudgetsQuery : IRequest<List<Budget>>;
}
