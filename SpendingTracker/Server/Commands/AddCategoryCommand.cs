using MediatR;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Server.Commands
{
    public record AddCategoryCommand(Category category) : IRequest<int>;
}