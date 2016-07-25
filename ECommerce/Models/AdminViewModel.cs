using ECommerce.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class AdminViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
    }
}