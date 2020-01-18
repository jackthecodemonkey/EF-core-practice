using DataAccess;
using Products.Data.Models;
using Products.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Products.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly ProductContext database;
        public ProductService(ProductContext productContext)
        {
            database = productContext;
        }

        public async Task<ICollection<Product>> RetrieveProducts(string? name)
        {
            if (name != null)
            {
                return await RetrieveProductsByName(name);
            }
            else 
            {
                return await RetrieveAllProducts();
            }

        }

        public async Task<Product> RetrieveProductById(Guid id)
        {
            return await database.Product.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> AddProduct(Product product)
        {
            var newProduct = database.Product.Add(product);
            var insertCount = await database.SaveChangesAsync();
            return insertCount == 0 ? null : newProduct.Entity;
        }

        public async Task<Product> UpdateProduct(Guid id, Product product)
        {
            if (product == null || id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(product), "No product id provided");
            }

            var existingProduct = await RetrieveProductById(id);
            if (existingProduct == null)
            {
                throw new Exception("Product Id not found");
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.DeliveryPrice = product.DeliveryPrice;

            await database.SaveChangesAsync();

            return existingProduct;
        }
        
        public async Task DeleteProduct(Guid id)
        {
            var existingProduct = await database.Product.SingleOrDefaultAsync(p => p.Id == id);
            if (existingProduct == null)
            {
                throw new Exception("Product not found");
            }
            database.Product.Remove(existingProduct);
            await database.SaveChangesAsync();
        }

        private async Task<ICollection<Product>> RetrieveProductsByName(string name = null)
        {
            return await database.Product.Where(p => p.Name == name).ToListAsync();
        }

        private async Task<ICollection<Product>> RetrieveAllProducts()
        {
            return await database.Product.ToListAsync();
        }      
    }
}
