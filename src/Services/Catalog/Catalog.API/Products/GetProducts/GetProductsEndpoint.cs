namespace Catalog.API.Products.GetProducts
{
    public record GetProductsRequest(int? PageNumber = 1, int? PageSize = 10);
    public record GetProductsResponse(IEnumerable<Product> Products);

    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            // Define the endpoint to get all products.
            app.MapGet("/products", async ([AsParameters] GetProductsRequest request, ISender sender) =>
            {
                // Map the HTTP request to the GetProductsQuery record.
                var query = request.Adapt<GetProductsQuery>();

                // Send the GetProductsQuery record to the GetProductsQueryHandler.
                var result = await sender.Send(query);

                // Map the GetProductsResult record to the HTTP response.
                var response = result.Adapt<GetProductsResponse>();

                // Return the HTTP response.
                return Results.Ok(response);
            })
            .WithName("GetProduct")
            .Produces<GetProductsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get all products.")
            .WithDescription("Get all products.");
        }
    }
}
