using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bangazon.Models.ProductViewModels
{
    public class ProductCreateVM
    {
        public Product Product { get; set; }
        public List<ProductType> Categories { get; set; }
        public List<SelectListItem> CategoryOptions
        {
            get
            {
                return Categories?.Select(c => new SelectListItem(c.Label, c.ProductTypeId.ToString())).ToList();
            }
        }
    }
}
