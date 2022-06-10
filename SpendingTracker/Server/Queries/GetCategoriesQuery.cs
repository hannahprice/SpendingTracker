using MediatR;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Server.Queries
{
    public record GetCategoriesQuery : IRequest<List<Category>>;
}
