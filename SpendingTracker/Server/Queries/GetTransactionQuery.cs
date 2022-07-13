using MediatR;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Server.Queries;

public record GetTransactionQuery(int id) : IRequest<Transaction>;