using Ardalis.GuardClauses;
using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers
{
    // TODO: Enhance routes and status reponses with best practices
    // TODO: Validate against null arguments
    // TODO: Improve logs
    // TODO: Handle api errors

    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly ILogger<CatalogController> logger;

        public CatalogController(
            IProductRepository productRepository,
            ILogger<CatalogController> logger)
        {
            this.productRepository = Guard.Against.Null(productRepository, nameof(productRepository));
            this.logger = Guard.Against.Null(logger, nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsAsync()
        {
            var products = await this.productRepository.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id:length(24)}", Name = nameof(GetProductByIdAsync))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductByIdAsync(string id)
        {
            this.logger.LogInformation($"Looking for product by Id: {id}");
            var product = await this.productRepository.GetProductByIdAsync(id);

            if (product == null)
            {
                this.logger.LogError($"Product not found. Id: {id}");
                return NotFound();
            }

            return Ok(product);
        }

        [Route("[action]/{category}", Name = nameof(GetProductByCategoryAsync))]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategoryAsync(string category)
        {
            this.logger.LogInformation($"Looking for product by Category: {category}.");
            var products = await this.productRepository.GetProductByCategoryAsync(category);
            return Ok(products);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> CreateProductAsync([FromBody] Product product)
        {
            this.logger.LogInformation("Creating product.", new { product });
            await this.productRepository.CreateProductAsync(product);

            return CreatedAtRoute(nameof(GetProductByIdAsync), new { id = product.Id }, product);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProductAsync([FromBody] Product product)
        {
            this.logger.LogInformation("Updating product.", new { product });
            return Ok(await this.productRepository.UpdateProductAsync(product));
        }

        [HttpDelete("{id:length(24)}", Name = nameof(DeleteProductAsync))]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProductAsync(string id)
        {
            this.logger.LogInformation($"Deleting product by Id: {id} .");
            return Ok(await this.productRepository.DeleteProductAsync(id));
        }
    }
}
