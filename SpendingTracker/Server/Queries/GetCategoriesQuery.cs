using MediatR;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Server.Queries
{
    public class GetCategoriesQuery : IRequest<List<Category>>
    {
    }
}
