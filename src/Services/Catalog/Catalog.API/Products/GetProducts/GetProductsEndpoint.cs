
namespace Catalog.API.Products.GetProducts
{
    //public record GetProductsRequest;
    public record GetProductsResponse(IEnumerable<Product> Products);

    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            // Define the endpoint to get all products.
            app.MapGet("/products", async (ISender sender) =>
            {
                // Send the GetProductsQuery record to the GetProductsQueryHandler.
                var result = await sender.Send(new GetProductsQuery());
                // Map the GetProductsResult record to the HTTP response.
                var response = new GetProductsResponse(result.Products);
                // Return the HTTP response.
                return Results.Ok(response);
            });
        }
    }
}
