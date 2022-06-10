using MediatR;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Server.Queries
{
    public record GetTransactionsQuery : IRequest<List<Transaction>>;
}
