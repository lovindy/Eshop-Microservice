using MediatR;

namespace BuildingBlocks.CQRS
{

    // ICommandHandler is a marker interface that represents a command handler
    // Purpose: To represent a command handler that handles a command and returns a response
    public interface ICommandHandler<in TCommand>
        : IRequestHandler<TCommand, Unit>
        where TCommand : ICommand<Unit>
    {
    }

    // ICommandHandler is a marker interface that represents a command handler
    // Purpose: To represent a command handler that handles a command and returns a response
    // Requires: TCommand must be a ICommand<TResponse> and TResponse must not be null
    public interface ICommandHandler<in TCommand, TResponse>
        : IRequestHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
        where TResponse : notnull
    {
    }
}
