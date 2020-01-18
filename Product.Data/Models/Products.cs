using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Products.Data.Models
{
    public class ProductCollection
    {
        public Collection<Product> Items { set; get; } = new Collection<Product>();

        public void AddRange(IEnumerable<Product> products)
        {
            foreach (var product in products)
            {
                Items.Add(product);
            }
        }
    }
}
