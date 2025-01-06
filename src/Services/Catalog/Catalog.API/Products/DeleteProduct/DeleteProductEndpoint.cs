
namespace Catalog.API.Products.DeleteProduct
{
    //public record DeleteProductRequest(Guid Id);
    public record DeleteProductResponse(bool IsSuccess);

    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            // Define the delete product endpoint
            app.MapDelete("products/{id}", async (Guid id, ISender sender) =>
            {
                // Send the DeleteProductCommand record to the DeleteProductCommandHandler.
                var result = await sender.Send(new DeleteProductCommand(id));

                // Map the DeleteProductResult record to the HTTP response.
                var response = result.Adapt<DeleteProductResponse>();

                // Return the HTTP response.
                return Results.Ok(response);
            })
            .WithName("DeleteProduct")
            .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete a product.")
            .WithDescription("Delete a product.");
        }
    }
}
