using ECommerce.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public int? Id { get; set; }
    }
    
}