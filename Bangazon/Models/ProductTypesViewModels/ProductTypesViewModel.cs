using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bangazon.Models.ProductTypesViewModels
{
    public class ProductTypesViewModel
    {
        public ProductType ProductType { get; set; }
        public List<GroupedProducts> GroupedProducts { get; set; } = new List<GroupedProducts>();
    }
}
