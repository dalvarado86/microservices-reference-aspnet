using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        private const string ConnectionStringEnv = "DatabaseSettings:ConnectionString";
        private const string DatabaseNameEnv = "DatabaseSettings:DatabaseName";
        private const string CollectionNameEnv = "DatabaseSettings:CollectionName";

        public CatalogContext(IConfiguration configuration)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var connectionString = configuration.GetValue<string>(ConnectionStringEnv);
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(DatabaseNameEnv);

            Products = database.GetCollection<Product>(CollectionNameEnv);

            CatalogContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
