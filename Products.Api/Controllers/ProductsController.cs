using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Products.Data.Models;
using Products.Services.Interfaces;

namespace Products.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IProductOptionService productOptionService;

        public ProductsController(IProductService productService, IProductOptionService productOptionService)
        {
            this.productService = productService;
            this.productOptionService = productOptionService;
        }

        /// <summary>
        /// Get all products or products matches with given name
        /// </summary>
        /// <param name="name">Name of a product</param>
        /// <returns>Products</returns>
        public async Task<ActionResult<ProductCollection>> Get(string name = null)
        {
            var products = await productService.RetrieveProducts(name);
            var productCollection = new ProductCollection();
            productCollection.AddRange(products);
            return productCollection;
        }

        /// <summary>
        /// Get a product matchs with the given id
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>A Product</returns>
        [HttpGet("{id}", Name = "GetById")]
        public async Task<ActionResult<Product>> GetById(Guid id)
        {
            return await productService.RetrieveProductById(id);
        }

        /// <summary>
        /// Add a new product
        /// </summary>
        /// <param name="product">product instance</param>
        /// <returns>Product Summary</returns>
        [HttpPost]
        public async Task<ActionResult<ProductSummary>> AddProduct([FromBody] Product product)
        {
            var newProduct = await productService.AddProduct(product);
            var productSummary = new ProductSummary()
            {
                Id = newProduct.Id,
                Name = newProduct.Name,
                Location = Url.Link("GetById", new { id = newProduct.Id })
            };
            return CreatedAtRoute("GetById", new { id = newProduct.Id }, productSummary);
        }

        /// <summary>
        /// Update a product
        /// </summary>
        /// <param name="id">product id</param>
        /// <param name="product">product to be updated</param>
        /// <returns>Product Summary</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductSummary>> Put(Guid id, [FromBody] Product product)
        {
            var updatedProduct = await productService.UpdateProduct(id, product);
            var productSummary = new ProductSummary()
            {
                Id = updatedProduct.Id,
                Name = updatedProduct.Name,
                Location = Url.Link("GetById", new { id = updatedProduct.Id })
            };
            return CreatedAtRoute("GetById", new { id = updatedProduct.Id }, productSummary);
        }

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="id">product id</param>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await productOptionService.RemoveProductOptions(id);
            await productService.DeleteProduct(id);
           return NoContent();
        }

    }
}
