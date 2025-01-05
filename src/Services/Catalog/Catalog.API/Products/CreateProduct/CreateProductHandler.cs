using System.Diagnostics.SymbolStore;
using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Marten;
using MediatR;

namespace Catalog.API.Products.CreateProduct
{

    // CreateProductCommand is a record that represents the data that the client sends to the server to create a product.
    public record CreateProductCommand(string Name, string Description, string ImageFile, decimal Price, List<string> Category)
        : ICommand<CreateProductResult>;

    // CreateProductResult is a record that represents the data that the server sends to the client after creating a product.
    public record CreateProductResult(Guid Id);

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
            // Skip this step for now.

            // 3. Return the product id.
            //return Task.FromResult(new CreateProductResult(product.Id));
            return new CreateProductResult(Guid.NewGuid());
        }
    }
}
