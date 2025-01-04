namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductRequest(string Name, string Description, string ImageFile, decimal Price, List<string> Category);
    public record CreateProductResponse(Guid Id);
    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            // TODO: Implement the HTTP POST method to create a product.
            // 1. Map the incoming request to the CreateProductRequest record.
            // 2. Send the CreateProductRequest record to the CreateProductRequestHandler.
            // 3. Map the CreateProductResponse record to the HTTP response.
            // 4. Return the HTTP response.

            // ** Create the endpoint to create a product.
            // ** MapPost is a method that maps an HTTP POST request to a specific endpoint.
            app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
            {
                // 1. Map the incoming request to the CreateProductRequest record.
                var command = request.Adapt<CreateProductCommand>();

                // 2. Send the CreateProductRequest record to the CreateProductRequestHandler.
                var result = await sender.Send(command);

                // 3. Map the CreateProductResponse record to the HTTP response.
                var response = result.Adapt<CreateProductResponse>();

                // 4. Return the HTTP response.
                return Results.Created($"/products/{response.Id}", response);
            })
            .WithName("CreateProduct") // ** WithName is a method that sets the name of the endpoint.
            .Produces<CreateProductRequest>() // ** Produces is a method that sets the response type of the endpoint.
            .ProducesProblem(StatusCodes.Status400BadRequest) // ** ProducesProblem is a method that sets the response type of the endpoint.
            .WithSummary("Create a product.") // ** WithSummary is a method that sets the summary of the endpoint.
            .WithDescription("Create a product."); // ** WithDescription is a method that sets the description of the endpoint.
        }
    }
}
