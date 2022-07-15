using MediatR;

namespace SpendingTracker.Server.Commands;

public record DeleteTransactionCommand(int id): IRequest;