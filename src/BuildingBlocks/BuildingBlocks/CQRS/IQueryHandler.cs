using MediatR;

namespace BuildingBlocks.CQRS
{
    // IQueryHandler is a marker interface that represents a query handler
    // Purpose: To represent a query handler that handles a query and returns a response
    // Requires: TQuery must be a query, TResponse must not be null
    public interface IQueryHandler<in TQuery, TResponse>
        : IRequestHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
        where TResponse : notnull
    {
    }
}
