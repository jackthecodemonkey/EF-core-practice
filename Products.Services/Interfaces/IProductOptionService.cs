using Products.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Products.Services.Interfaces
{
    public interface IProductOptionService
    {
        Task<ICollection<ProductOption>> RetrieveProductOptions(Guid id);
        Task<ProductOption> RetrieveProductOption(Guid productId, Guid optionId);
        Task<ProductOption> AddProductOption(Guid productId, ProductOption productOption);
        Task<ProductOption> UpdateProductOption(Guid productId, Guid optionId, ProductOption productOption);
        Task DeleteProductOption(Guid productId, Guid optionId);
        Task RemoveProductOptions(Guid productId);
    }
}
