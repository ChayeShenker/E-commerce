using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.data
{
    public class CartRepository
    {
        string _connectionString;
        public CartRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public int NewCart()
        {
            using(var context = new ECommerceDataContext(_connectionString))
            {
                Cart c = new Cart();
                c.DateCreated = DateTime.Now;
                context.Carts.InsertOnSubmit(c);
                context.SubmitChanges();
                return c.CartId;
            }
        }
        public void AddCartItem(CartItem cI)
        {
            using (var context = new ECommerceDataContext(_connectionString))
            {
                context.CartItems.InsertOnSubmit(cI);
                context.SubmitChanges();
            }
        }
        public IEnumerable<CartItem> GetItems(int cartId)
        {
            using (var context = new ECommerceDataContext(_connectionString))
            {
                var dataLoadOptions = new DataLoadOptions();
                dataLoadOptions.LoadWith<CartItem>(c => c.Product);
                dataLoadOptions.LoadWith<Product>(p => p.Images);
                context.LoadOptions = dataLoadOptions;

                return context.CartItems.Where(c => c.CartId == cartId).ToList();
            }
        }
        public int CartItemsCount(int cartId)
        {
            using (var context = new ECommerceDataContext(_connectionString))
            {
                
              
                if (cartId != 0)
                {
                    IEnumerable<CartItem> items = context.CartItems.Where(c => c.CartId == cartId);
                    if(items != null)
                    {
                        return items.Sum(i => i.Quantity);
                    }
                    else
                    {
                        return 0;
                    }
                }


                else
                {
                    return 0;
                }
                
                
                
            }
        }
        public void Remove(int itemId)
        {
            using(var context = new ECommerceDataContext(_connectionString))
            {
                context.ExecuteCommand("DELETE * FROM CartItems Where ItemId = {0}", itemId);
            }
        }
        public void UpdateItem(CartItem cartItem)
        {
            using(var context = new ECommerceDataContext(_connectionString))
            {
                context.CartItems.Attach(cartItem);
                context.Refresh(RefreshMode.KeepCurrentValues, cartItem);
                context.SubmitChanges();
              
            }
        }
        public CartItem AlreadyAdded(int productId, int cartId)
        {
            using(var context = new ECommerceDataContext(_connectionString))
            {
                return context.CartItems.FirstOrDefault(c => c.CartId == cartId && c.ProductId == productId);
            }
        }
        public void DeleteItem(int itemId)
        {
            using(var context = new ECommerceDataContext(_connectionString))
            {
                context.ExecuteCommand("DELETE FROM CartItems WHERE ItemId = {0}",itemId);
              
            }
        }
    }
}
