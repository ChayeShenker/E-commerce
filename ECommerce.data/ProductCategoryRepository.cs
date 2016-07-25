using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.data
{
    public class ProductCategoryRepository
    {
        private string _connectionString;
        public ProductCategoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Category> GetCategories()
        {
            using(var context = new ECommerceDataContext(_connectionString))
            {
                return context.Categories.ToList();
            }
        }

        public IEnumerable<Product>GetProcuctsByCategory(int? categoryId)
        {
            using (var context = new ECommerceDataContext(_connectionString))
            {
                var dataLoadOptions = new DataLoadOptions();
                dataLoadOptions.LoadWith<Product>(p => p.Images);
                context.LoadOptions = dataLoadOptions;
                return context.Products.Where(p => p.CategoryId == categoryId).ToList();
                    
            }
        }
        public Product GetProductById(int id)
        {
            using (var context = new ECommerceDataContext(_connectionString))
            {
                var dataLoadOptions = new DataLoadOptions();
                dataLoadOptions.LoadWith<Product>(p => p.Images);
                context.LoadOptions = dataLoadOptions;
                return context.Products.Where(p => p.ProductId == id).First();
            }
        }
    }
}
