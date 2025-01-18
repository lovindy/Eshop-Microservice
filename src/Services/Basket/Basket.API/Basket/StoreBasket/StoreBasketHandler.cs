using FluentValidation;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(bool IsSuccess);

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null.");
            RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("Username is required.");
        }
    }

    public class StoreBasketCommandHandler : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
