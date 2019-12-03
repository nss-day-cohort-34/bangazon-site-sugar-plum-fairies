using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bangazon.Models.ProductTypesViewModels
{
    public class ProductTypesViewModel
    {
        public Product Product { get; set; } = new Product();
        public ProductType ProductType { get; set; }
        public List<GroupedProducts> GroupedProducts { get; set; } = new List<GroupedProducts>();
    }
}
