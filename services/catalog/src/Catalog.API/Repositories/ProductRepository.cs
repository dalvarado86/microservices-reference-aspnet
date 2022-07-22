using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext context;

        public ProductRepository(ICatalogContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateProductAsync(Product product)
        {
            await this.context.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProductAsync(string id)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);

            var deleteResult = await this.context.Products
                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<Product>> GetProductByCategoryAsync(string category)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Category, category);

            return await this.context.Products
                .Find(filter)
                .ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            return await this.context.Products
                .Find(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByNameAsync(string name)
        {
            var filter = Builders<Product>.Filter.ElemMatch(p => p.Name, name);

            return await this.context.Products
                .Find(filter)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await this.context.Products
                .Find(p => true)
                .ToListAsync();
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            var updateResult = await this.context.Products
                .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
    }
}
