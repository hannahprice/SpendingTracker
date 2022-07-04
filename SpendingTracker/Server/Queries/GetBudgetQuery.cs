using MediatR;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Server.Queries
{
    public record GetBudgetQuery(int id) : IRequest<Budget>;
}