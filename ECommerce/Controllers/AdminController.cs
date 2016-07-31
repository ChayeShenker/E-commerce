using ECommerce.data;
using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ECommerce.Controllers
{
    public class AdminController : Controller
    {

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            AdministratorRepository repo = new AdministratorRepository(Properties.Settings.Default.ConStr);
            Administrator admin = repo.Login(username, password);
            if (admin == null)
            {
                return Redirect("/account/signin");
            }

            FormsAuthentication.SetAuthCookie(username, true);
            ViewBag.IsAdmin = true;
            return Redirect("/admin/addcategory");
        }
       [Authorize]
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult AddCategory(Category c)
        {
            AdministratorRepository repo = new AdministratorRepository(Properties.Settings.Default.ConStr);
            repo.AddCategory(c);
            return Redirect("/admin/addcategory");
        }
       [Authorize]
        public ActionResult AddProduct()
        {
            AdministratorRepository repo = new AdministratorRepository(Properties.Settings.Default.ConStr);
            AdminViewModel viewModel = new AdminViewModel();
            viewModel.Categories =repo.GetCategories();
            return View(viewModel);
        }
        [HttpPost]
        [Authorize]
       public ActionResult AddProduct(HttpPostedFileBase[] pictures, Product p)
        {
            
            AdministratorRepository repo = new AdministratorRepository(Properties.Settings.Default.ConStr);
            repo.AddProduct(p);
            foreach (HttpPostedFileBase pic in pictures)
            {
                if(pic != null)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(pic.FileName);
                    pic.SaveAs(Server.MapPath("~/Images/") + fileName);
                    Image image = new Image();
                    image.ImageName = fileName;
                    image.ProductId = p.ProductId;
                    repo.AddImage(image);
                }
               
            }
              
            
            return Redirect("/admin/addproduct");
        }
    
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }


    }
}
