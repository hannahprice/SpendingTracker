using MediatR;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Server.Queries
{
    public class GetBudgetsQuery : IRequest<List<Budget>>
    {
    }
}
