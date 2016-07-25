using ECommerce.data;
using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Controllers
{
    public class HomeController : Controller
    {
      
        public ActionResult Index(int? categoryId)
        {
            if(categoryId == null)
            {
                categoryId = 1;
            }
            IndexViewModel viewModel = new IndexViewModel();
            ProductCategoryRepository repo = new ProductCategoryRepository(Properties.Settings.Default.ConStr);
            viewModel.Categories = repo.GetCategories();
            viewModel.Products = repo.GetProcuctsByCategory(categoryId);
            viewModel.Id = categoryId;
            return View(viewModel);
        }
        public ActionResult Product(int productId)
        {
            ProductViewModel viewModel = new ProductViewModel();
            ProductCategoryRepository repo = new ProductCategoryRepository(Properties.Settings.Default.ConStr);
            viewModel.product = repo.GetProductById(productId);
            viewModel.Categories = repo.GetCategories();
            
            return View(viewModel);
        }
    }
}
