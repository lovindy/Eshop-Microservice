namespace Catalog.API.Products.UpdateProduct
{

    public record UpdateProductCommand(Guid Id, string Name, string Description, string ImageFile, decimal Price, List<string> Category)
        : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);

    internal class UpdateProductCommandHandler
        (IDocumentSession session, ILogger<UpdateProductCommandHandler> logger)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {

            // TODO: Implement the logic to update a product.

            // Log the update operation.
            logger.LogInformation("Updating product with id {Id}", command.Id);

            // Load the product from the database.
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

            // Check if the product exists.
            if (product is null)
            {
                // Log the error.
                logger.LogError("Product with id {Id} not found", command.Id);
                throw new ProductNotFoundException();
            }

            // Update the product.
            product.Name = command.Name;
            product.Description = command.Description;
            product.ImageFile = command.ImageFile;
            product.Price = command.Price;
            product.Category = command.Category;

            // Save the product to the database.
            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);

            // Return the result.
            return new UpdateProductResult(true);
        }
    }
}
