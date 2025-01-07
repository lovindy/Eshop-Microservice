using FluentValidation;

namespace Catalog.API.Products.CreateProduct
{

    // CreateProductCommand is a record that represents the data that the client sends to the server to create a product.
    public record CreateProductCommand(string Name, string Description, string ImageFile, decimal Price, List<string> Category)
        : ICommand<CreateProductResult>;

    // CreateProductResult is a record that represents the data that the server sends to the client after creating a product.
    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {

            // RuleFor is a method that defines a rule for a property.
            // 1. The Name property should not be empty.
            // 2. The Description property should not be empty.
            // 3. The ImageFile property should not be empty.
            // 4. The Price property should be greater than 0.
            // 5. The Category property should not be empty.

            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price should be greater than 0.");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required.");
        }
    }

    // CreateProductHandler is a class that contains the logic to create a product.
    internal class CreateProductCommandHandler(IDocumentSession session)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        // Handle is a method that contains the logic to create a product.
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            // TODO: Implement the logic to create a product.
            // 1. Create a new product.
            // 2. Save the product to the database.
            // 3. Return the product id.

            // 1. Create a new product.
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price,
                Category = command.Category
            };

            // 2. Save the product to the database.
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            // 3. Return the product id.
            //return Task.FromResult(new CreateProductResult(product.Id));
            return new CreateProductResult(Guid.NewGuid());
        }
    }
}
