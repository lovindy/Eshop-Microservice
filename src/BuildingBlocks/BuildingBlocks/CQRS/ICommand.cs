using MediatR;

namespace BuildingBlocks.CQRS
{
    // ICommand is a marker interface that represents a command
    // Purpose: To represent a command with no return 
    public interface ICommand : ICommand<Unit>
    {
    }

    // ICommand is a marker interface that represents a command
    // Purpose: To represent a command with a return value
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
