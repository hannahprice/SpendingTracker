using MediatR;

namespace SpendingTracker.Server.Commands;

public record DeleteSubcategoryCommand(int id): IRequest;