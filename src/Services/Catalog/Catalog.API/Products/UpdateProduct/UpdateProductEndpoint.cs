
namespace Catalog.API.Products.UpdateProduct
{

    public record UpdateProductRequest(Guid Id, string Name, string Description, string ImageFile, decimal Price, List<string> Category);
    public record UpdateProductResponse(bool IsSuccess);

    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            // Todo: Implement the HTTP PUT method to update a product.
            // 1. Map the incoming request to the UpdateProductRequest record.
            // 2. Send the UpdateProductRequest record to the UpdateProductRequestHandler.
            // 3. Map the UpdateProductResponse record to the HTTP response.
            // 4. Return the HTTP response.

            app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
            {
                // 1. Map the incoming request to the UpdateProductRequest record
                var command = request.Adapt<UpdateProductCommand>();

                // 2. Send the UpdateProductRequest record to the UpdateProductRequestHandler.
                var result = await sender.Send(command);

                // 3. Map the UpdateProductResponse record to the HTTP response.
                var response = result.Adapt<UpdateProductResponse>();

                // 4. Return the HTTP response.
                return Results.Ok(response);
            })
            .WithName("UpdateProduct")
            .Produces<UpdateProductRequest>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Update a product.")
            .WithDescription("Update a product.");
        }
    }
}
