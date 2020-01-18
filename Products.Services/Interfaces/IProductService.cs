using Products.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Products.Services.Interfaces
{
    public interface IProductService
    {
        Task<ICollection<Product>> RetrieveProducts(string name = null);
        Task<Product> RetrieveProductById(Guid id);
        Task<Product> AddProduct(Product product);
        Task<Product> UpdateProduct(Guid id,Product product);
        Task DeleteProduct(Guid id);
    }
}
