
namespace Catalog.API.Products.GetProducts
{
    public record GetProductsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<Product> Products);

    internal class GetProductsQueryHandler
        (IDocumentSession session)
        : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            // Get all products from the database.
            var products = await session.Query<Product>().ToListAsync(cancellationToken);

            // Return the products.
            return new GetProductsResult(products);
        }
    }
}
