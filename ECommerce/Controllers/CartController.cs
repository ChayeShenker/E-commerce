using ECommerce.data;
using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Controllers
{
    public class CartController : Controller
    {

        public ActionResult Index()
        {
            int cartId = 0;
            if (Session["cartId"] != null)
            {
                cartId = (int)Session["cartId"];

            }
            CartRepository repo = new CartRepository(Properties.Settings.Default.ConStr);
            CartViewModel viewModel = new CartViewModel();
            viewModel.Items = repo.GetItems(cartId);
            return View(viewModel);
           
        }
    [HttpPost]
        public void Add(int productId, int quantity)
        {
            CartRepository repo = new CartRepository(Properties.Settings.Default.ConStr);
            if(Session["cartId"]== null)
            {
                
                int id = repo.NewCart();
                Session["cartId"] = id;
                
            }
            CartItem cartItem = new CartItem();
            cartItem.CartId = (int)Session["cartId"];
            cartItem.ProductId = productId;
            cartItem.Quantity = quantity;
            repo.AddCartItem(cartItem);
        }
    [HttpPost]
        public void UpdateItem(CartItem item, int newQuantity)
        {
            item.Quantity += newQuantity;
            CartRepository repo = new CartRepository(Properties.Settings.Default.ConStr);
            repo.UpdateItem(item);
        }
        [HttpPost]
        public void UpdateQuantity(CartItem item)
        {
            CartRepository repo = new CartRepository(Properties.Settings.Default.ConStr);
            repo.UpdateItem(item);
        }
        public ActionResult GetCartCount()
        {
            int cartId = (int)Session["cartId"];
            CartRepository repo = new CartRepository(Properties.Settings.Default.ConStr);
            return Json(repo.CartItemsCount(cartId), JsonRequestBehavior.AllowGet);
        }
       
        public ActionResult AlreadyAdded(int productId)
        {
           
            if (Session["cartId"] == null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            int cartId = (int)Session["cartId"];
            CartRepository repo = new CartRepository(Properties.Settings.Default.ConStr);

            CartItem item = repo.AlreadyAdded(productId, cartId);


            if (item == null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);

            }
            var jsonItem = new
            {
                ProductId = item.ProductId,
                CartId = item.CartId,
                ItemId = item.ItemId,
                Quantity = item.Quantity
            };
            return Json(jsonItem, JsonRequestBehavior.AllowGet);
            
        }
        [HttpPost]
        public ActionResult Delete(int itemId)
        {
            CartRepository repo = new CartRepository(Properties.Settings.Default.ConStr);
            repo.DeleteItem(itemId);
            return Redirect("/cart/Index?cartId=" + (int)Session["cartId"]);
        }
        

    }
}
