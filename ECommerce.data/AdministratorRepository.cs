using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.data
{
    public class AdministratorRepository
    {
        private string _connectionString;
        public AdministratorRepository (string connectionString)
	{
        _connectionString = connectionString;
	}
        public void AddAdmin(string username, string password)
        {
            string salt = PasswordManager.GenerateSalt();
            string passwordHash = PasswordManager.HashPassword(password, salt);
            using (var context = new ECommerceDataContext())
            {
                context.Administrators.InsertOnSubmit(new Administrator { UserName = username, PasswordHash = passwordHash, PasswordSalt = salt });
                context.SubmitChanges();
            }
        }
        public Administrator Login(string username, string password)
        {   Administrator a = GetAdmin(username, password);
            
            string passwordHash = PasswordManager.HashPassword(password, a.PasswordSalt);
            bool isMatch = PasswordManager.IsMatch(password, passwordHash, a.PasswordSalt);
            if(a == null)
            {
                return null;
            }
            if (isMatch)
            {
                return a;
            }
            return null;
        }
        private Administrator GetAdmin(string username, string password)
        {
            using(ECommerceDataContext context = new ECommerceDataContext(_connectionString))
            {
                return context.Administrators.Where(a => a.UserName == username).First();
            }
        }
        public void AddCategory(Category c)
        {
            using (ECommerceDataContext context = new ECommerceDataContext(_connectionString))
            {
                context.Categories.InsertOnSubmit(c);
                context.SubmitChanges();
            }
        }
        public void AddProduct(Product p)
        {
            using (ECommerceDataContext context = new ECommerceDataContext(_connectionString))
            {
                context.Products.InsertOnSubmit(p);
                context.SubmitChanges();
            }
        }
        public void AddImage(Image i)
        {
            using (ECommerceDataContext context = new ECommerceDataContext(_connectionString))
            {
                context.Images.InsertOnSubmit(i);
                context.SubmitChanges();
            }
        }
        public IEnumerable<Category> GetCategories()
        {
            using (ECommerceDataContext context = new ECommerceDataContext(_connectionString))
            {
                return context.Categories.ToList();
            }
        }

    }
}
