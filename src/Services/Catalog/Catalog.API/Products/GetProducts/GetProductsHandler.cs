
namespace Catalog.API.Products.GetProducts
{
    public record GetProductsQuery : IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<Product> Products);

    internal class GetProductsQueryHandler
        (IDocumentSession session, ILogger<GetProductsQueryHandler> logger)
        : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            // Log that we are getting products from the database.
            logger.LogInformation("Getting products from the database...");

            // Get all products from the database.
            var products = await session.Query<Product>().ToListAsync(cancellationToken);

            // Return the products.
            return new GetProductsResult(products);
        }
    }
}
