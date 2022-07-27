using MediatR;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Server.Queries;

public record GetSubcategoryQuery(int id):IRequest<Subcategory>;