using MediatR;

namespace Catalog.API.Products.CreateProduct
{

    // CreateProductCommand is a record that represents the data that the client sends to the server to create a product.
    public record CreateProductCommand(string Name, string Description, string ImageFile, decimal Price, List<string> Category) : IRequest<CreateProductResult>;

    // CreateProductResult is a record that represents the data that the server sends to the client after creating a product.
    public record CreateProductResult(Guid Id);

    // CreateProductHandler is a class that contains the logic to create a product.
    internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
    {
        // Handle is a method that contains the logic to create a product.
        public Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
