using ECommerce.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class ProductViewModel
    {
        public Product product { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        
    }
}