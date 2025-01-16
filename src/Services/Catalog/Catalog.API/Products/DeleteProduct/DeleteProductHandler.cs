namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id)
        : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSuccess);

    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
        }
    }

    internal class DeleteProductCommandHandler
        (IDocumentSession session)
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            // Load the product from the database.
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

            // Check if the product exists.
            if (product is null)
            {
                // Log the error.       
                throw new ProductNotFoundException(command.Id);
            }

            // Delete the product from the database.
            session.Delete(product);
            await session.SaveChangesAsync(cancellationToken);

            // Return the result.
            return new DeleteProductResult(true);
        }
    }
}
