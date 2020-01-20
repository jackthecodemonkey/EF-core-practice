using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Products.Data.Models
{
    public class ProductOption
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [JsonIgnore]
        public virtual Product Product { get; set; } = null;

        public ProductOption Clone()
        {
            return new ProductOption()
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description
            };
        }
    }
}
