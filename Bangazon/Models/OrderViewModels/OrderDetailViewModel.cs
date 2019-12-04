using System.Collections.Generic;
using System.Linq;

namespace Bangazon.Models.OrderViewModels
{
    public class OrderDetailViewModel
    {
        public Order Order { get; set; }

        public List<OrderLineItem> LineItems { get; set; } = new List<OrderLineItem>();
    }
}