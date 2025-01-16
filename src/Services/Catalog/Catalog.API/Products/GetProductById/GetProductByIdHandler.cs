namespace Catalog.API.Products.GetProductById
{

    public record GetProductByIdQuery(Guid id) : IQuery<GetProductByIdResult>;

    public record GetProductByIdResult(Product Product);

    internal class GetProductByIdQueryHandler
        (IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger)
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            // Log that we are getting a product from the database.
            logger.LogInformation("Getting product from the database...");

            // Get the product from the database.
            var product = await session.LoadAsync<Product>(query.id, cancellationToken);

            // If the product is not found, throw a ProductNotFoundException.
            if (product is null)
            {
                // Log that the product was not found.
                throw new ProductNotFoundException(query.id);
            }

            // Return the product.
            return new GetProductByIdResult(product);
        }
    }
}
