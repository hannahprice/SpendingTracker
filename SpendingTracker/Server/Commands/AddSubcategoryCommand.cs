using MediatR;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Server.Commands
{
    public record AddSubcategoryCommand(Subcategory subcategory) : IRequest<int>;
}