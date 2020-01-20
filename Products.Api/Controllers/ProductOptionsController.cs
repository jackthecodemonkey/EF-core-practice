using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using Products.Data.Models;
using Products.Services.Interfaces;

namespace Products.Api.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductOptionsController : ControllerBase
    {
        private readonly IProductOptionService productOptionService;

        public ProductOptionsController(IProductOptionService productOptionService)
        {
            this.productOptionService = productOptionService;
        }

        [HttpGet("{id}/options", Name= "RetrieveProductOptions")]
        public async Task<ActionResult<ProductOptions>> RetrieveProductOptions(Guid id)
        {
            var productOptions = await productOptionService.RetrieveProductOptions(id);
            var productOptionCollection = new ProductOptions();
            productOptionCollection.AddRange(productOptions);
            return productOptionCollection;
        }

        [HttpGet("{id}/options/{optionId}", Name= "RetrieveProdctOption")]
        public async Task<ActionResult<ProductOption>> RetrieveProdctOption(Guid id, Guid optionId)
        {
            return await productOptionService.RetrieveProductOption(id, optionId);
        }
         
        [HttpPost("{id}/options")]
        public async Task<ActionResult<ProductOption>> PostProductOption(Guid id, ProductOption productOption)
        {
            var newProductOption = await productOptionService.AddProductOption(id, productOption);
            var productSummary = new ProductSummary()
            {
                Id = newProductOption.Id,
                Name = newProductOption.Name,
                Location = Url.Link("RetrieveProductOptions", new { id = newProductOption.Id })
            };
            return CreatedAtRoute("RetrieveProductOptions", new { id = newProductOption.Id }, productSummary);
        }

        /// <summary>
        /// Update a product option
        /// </summary>
        /// <param name="id">product id</param>
        /// <param name="optionId">product option id</param>
        /// <param name="productOption">product to be updated</param>
        /// <returns>Product Summary</returns>
        [HttpPut("{id}/options/{optionId}")]
        public async Task<ActionResult<ProductSummary>> Put(Guid id, Guid optionId, [FromBody] ProductOption productOption)
        {
            var updatedProductOption = await productOptionService.UpdateProductOption(id, optionId, productOption);
            var productSummary = new ProductSummary()
            {
                Id = updatedProductOption.Id,
                Name = updatedProductOption.Name,
                Location = Url.Link("RetrieveProdctOption", new { id, optionId = updatedProductOption.Id })
            };
            return CreatedAtRoute("RetrieveProdctOption", new { id, optionId = updatedProductOption.Id }, productSummary);
        }

        /// <summary>
        /// Delete a product option
        /// </summary>
        /// <param name="id">product id</param>
        /// <param name="optionId">product option id</param>
        [HttpDelete("{id}/options/{optionId}")]
        public async Task<ActionResult> Delete(Guid id, Guid optionId)
        {
            await productOptionService.DeleteProductOption(id, optionId);
            return NoContent();
        }

    }
}
