using Marten;
using Marten.Schema;

namespace Catalog.API.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            // Create a new session.
            using var session = store.LightweightSession();

            // Check if the database has any products.
            if (await session.Query<Product>().AnyAsync())
                return;

            // Store preconfigured products in the session.
            var preconfiguredProducts = GetPreconfiguredProducts();
            session.Store(preconfiguredProducts);

            // Save changes to the database.
            await session.SaveChangesAsync(cancellation);
        }

        // Define preconfigured products.
        private static IEnumerable<Product> GetPreconfiguredProducts() => new List<Product>
        {
            new Product
            {
                Id = new Guid("b1f264f6-4d07-49a3-9129-7b25db961a2b"),
                Name = "Wireless Mouse",
                Description = "Ergonomic wireless mouse with customizable buttons.",
                ImageFile = "wireless_mouse.jpg",
                Price = 29.99m,
                Category = new List<string> { "Electronics", "Accessories" }
            },
            new Product
            {
                Name = "Standing Desk",
                Description = "Height-adjustable standing desk with a spacious work surface.",
                ImageFile = "standing_desk.jpg",
                Price = 349.99m,
                Category = new List<string> { "Furniture", "Office" }
            }
        };
    }
}
