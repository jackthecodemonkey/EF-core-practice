using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Products.Data.Models
{
    public class ProductSummary
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        [Url]
        public string Location { get; set; }
    }
}
