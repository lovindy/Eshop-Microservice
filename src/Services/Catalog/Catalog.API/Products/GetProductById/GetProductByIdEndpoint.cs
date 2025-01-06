using Catalog.API.Products.GetProductById;

namespace Catalog.API.Products.NewFolder
{
    //public record GetProductByIdRequest();
    public record GetProductByIdResponse(Product Product);

    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            // Define the endpoint to get a product by id.
            app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
            {
                // Send the GetProductsQuery record to the GetProductsQueryHandler.
                var result = await sender.Send(new GetProductByIdQuery(id));

                // Map the GetProductsResult record to the HTTP response.
                var response = result.Adapt<GetProductByIdResponse>();

                // Return the HTTP response.
                return Results.Ok(response);
            })
            .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get product by id.")
            .WithDescription("Get product by id.");
        }
    }
}
