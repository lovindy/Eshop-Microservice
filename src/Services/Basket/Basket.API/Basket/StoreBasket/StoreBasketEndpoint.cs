
namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketRequest(ShoppingCart Cart);
    public record StoreBasketResponse(string Username);
    public class StoreBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) =>
            {
                // Map the request to the command
                var command = request.Adapt<StoreBasketCommand>();

                // Store the basket
                var result = await sender.Send(command);

                // Map the result to the response
                var response = result.Adapt<StoreBasketResponse>();

                // Return the response
                return Results.Created($"/basket/{response.Username}", response);
            })
            .WithName("StoreBasket")
            .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Store the basket for the specified user")
            .WithDescription("Stores the basket for the specified user.");
        }
    }
}
