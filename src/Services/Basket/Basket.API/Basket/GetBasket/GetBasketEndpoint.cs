namespace Basket.API.Basket.GetBasket
{

    //public record GetBasketRequest(string Username);
    public record GetBasketResponse(ShoppingCart Cart);

    public class GetBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            // Implement the GetBasket endpoint
            app.MapGet("/basket/{userName}", async (string userName, ISender Sender) =>
            {
                // Get the basket from the database
                var result = await Sender.Send(new GetBasketQuery(userName));

                // Map the result to the response
                var response = result.Adapt<GetBasketResponse>();

                // Return the response
                return Results.Ok(response);
            })
            .WithName("GetBasket")
            .Produces<GetBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get the basket for the specified user")
            .WithDescription("Returns the basket for the specified user.");
        }
    }
}
