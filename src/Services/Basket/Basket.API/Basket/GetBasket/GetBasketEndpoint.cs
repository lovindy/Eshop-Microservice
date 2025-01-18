namespace Basket.API.Basket.GetBasket
{

    //public record GetBasketRequest(string Username);
    public record GetBasketResponse(ShoppingCart Cart);

    public class GetBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            throw new NotImplementedException();
        }
    }
}
