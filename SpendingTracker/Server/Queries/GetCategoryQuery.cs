using MediatR;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Server.Queries;

public record GetCategoryQuery(int id): IRequest<Category>;