using MediatR;

namespace SpendingTracker.Server.Commands;

public record DeleteBudgetCommand(int id) : IRequest;