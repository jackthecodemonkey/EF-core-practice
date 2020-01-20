using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Products.Data.Models
{
    public class ProductOptions
    {
        public Collection<ProductOption> Items { set; get; } = new Collection<ProductOption>();
        public void AddRange(IEnumerable<ProductOption> prodctOptions)
        {
            foreach (var productOption in prodctOptions)
            {
                Items.Add(productOption);
            }
        }
    }
}
