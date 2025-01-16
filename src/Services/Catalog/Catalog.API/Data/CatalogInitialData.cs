using Marten.Schema;

namespace Catalog.API.Data
{
    public class CatalogInitialData : IInitialData
    {
        public Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
    }
}
