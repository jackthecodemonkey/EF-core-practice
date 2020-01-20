using DataAccess;
using Microsoft.EntityFrameworkCore;
using Products.Data.Models;
using Products.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products.Services.Implementation
{
    public class ProductOptionService : IProductOptionService
    {
        private readonly ProductContext database;
        private readonly IProductService productService;
        public ProductOptionService(ProductContext productContext, IProductService productService)
        {
            database = productContext;
            this.productService = productService;
        }

        public async Task<ProductOption> AddProductOption(Guid id, ProductOption productOption)
        {
            var exisitingProduct = await productService.RetrieveProductById(id);
            if (exisitingProduct == null)
            {
                throw new Exception("Product not found");
            }
            productOption.Id = Guid.NewGuid();
            productOption.Product = exisitingProduct;
            var newProductOption = database.ProductOption.Add(productOption);
            var insertCount = await database.SaveChangesAsync();
            return insertCount == 0 ? null : newProductOption.Entity;
        }

        public async Task DeleteProductOption(Guid productId, Guid optionId)
        {
            var exisitingProductOption = await RetrieveProductOption(productId, optionId);
            if (exisitingProductOption == null)
            {
                throw new Exception("Product option not found");
            }
            database.ProductOption.Remove(exisitingProductOption);
            await database.SaveChangesAsync();
        }

        public async Task<ProductOption> RetrieveProductOption(Guid productId, Guid optionId)
        {
            var exisitingProduct = await productService.RetrieveProductById(productId);
            if (exisitingProduct == null)
            {
                throw new Exception("Product not found");
            }
            var productOption = await database.ProductOption.AsNoTracking().FirstOrDefaultAsync(p => p.Product.Id == productId && p.Id == optionId);
            return productOption.Clone();
        }

        public async Task<ICollection<ProductOption>> RetrieveProductOptions(Guid id)
        {
            return await database.ProductOption.Where(p => p.Product.Id == id).ToListAsync();
        }

        public async Task<ProductOption> UpdateProductOption(Guid productId, Guid optionId, ProductOption productOption)
        {
            if (productId == null || optionId == null)
            {
                throw new ArgumentNullException("No product id or product option id provided");
            }
            var exisitingProductOption = await RetrieveProductOption(productId, optionId);
            if (exisitingProductOption == null)
            {
                throw new Exception("Product option not found");
            }
            exisitingProductOption.Name = productOption.Name;
            exisitingProductOption.Description = productOption.Description;

            database.Entry(exisitingProductOption).State = EntityState.Modified;
            await database.SaveChangesAsync();

            return exisitingProductOption;
        }

        public async Task RemoveProductOptions(Guid prodctId)
        {
            var options = await RetrieveProductOptions(prodctId);
            if(options != null)
            {
                database.ProductOption.RemoveRange(database.ProductOption.Where(o => o.Id == prodctId));
                database.SaveChanges();
            }
            return;
        }
    }
}
