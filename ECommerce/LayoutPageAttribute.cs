using ECommerce.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Providers.Entities;

namespace ECommerce
{
    public class LayoutPageAttribute : ActionFilterAttribute
    {
        CartRepository repo = new CartRepository(Properties.Settings.Default.ConStr);
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            int cartId = 0;
            if(filterContext.HttpContext.Session.Contents["cartId"]!= null)
            {
                cartId = (int)filterContext.HttpContext.Session.Contents["cartId"];  
            }
             
           
            filterContext.Controller.ViewBag.CartQuantity = repo.CartItemsCount(cartId);
            base.OnActionExecuting(filterContext);
        }
    }
}