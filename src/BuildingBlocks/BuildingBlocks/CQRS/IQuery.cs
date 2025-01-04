using MediatR;

namespace BuildingBlocks.CQRS
{
    // IQuery is a marker interface that represents a query
    // Purpose: To represent a query with a return value, but no side effects
    // Requires: TResponse must not be null
    public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : notnull
    {
    }
}
