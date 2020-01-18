using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Products.Data.Models
{
    public class ProductOptions
    {
        public Collection<ProductOption> Items { set; get; } = new Collection<ProductOption>();
    }
}
