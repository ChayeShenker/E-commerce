using ECommerce.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class CartViewModel
    {
        public IEnumerable<CartItem> Items { get; set; }
    }
}