using MediatR;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Server.Commands
{
    public record AddTransactionCommand(Transaction transaction) : IRequest<int>;
}