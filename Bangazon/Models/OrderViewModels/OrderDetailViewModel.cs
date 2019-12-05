using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Bangazon.Models.OrderViewModels
{
    public class OrderDetailViewModel
    {
        public Order Order { get; set; }

        public List<OrderLineItem> LineItems { get; set; } = new List<OrderLineItem>();
        public int PaymentTypeId { get; set; }
        public List<PaymentType> PaymentTypes { get; set; } = new List<PaymentType>();
        public List<SelectListItem> PaymentOptions => PaymentTypes.Select(pt => new SelectListItem()
        {
            Text = $"{pt.Description.Split("")[0]} ****{pt.AccountNumber.Substring(pt.AccountNumber.Length - 4)}",
            Value = $"{pt.PaymentTypeId}"
        }).ToList();
    }
}