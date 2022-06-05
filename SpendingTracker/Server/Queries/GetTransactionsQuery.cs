using MediatR;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Server.Queries
{
    public class GetTransactionsQuery : IRequest<List<Transaction>>
    {
    }
}
