using MediatR;

namespace SpendingTracker.Server.Commands;

public record DeleteCategoryCommand(int id) : IRequest;