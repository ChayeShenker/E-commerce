using ECommerce.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administrator.data
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a username");
            string username = Console.ReadLine();
            Console.WriteLine("Enter a password");
            string password = Console.ReadLine();
            AdministratorRepository repo = new AdministratorRepository(Properties.Settings.Default.ConStr);

            repo.AddAdmin(username, password);
            Console.ReadKey(true);
        }
    }
}
