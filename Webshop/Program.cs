using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace Webshop;
internal class Program
{

    static void Main(string[] args)
    {
        // Gets admin and user functions to use in main
        //Admin admin = new Admin();
        //User user = new User();

        while (true)
        {
            Console.WriteLine("Welcome to the store system!");
            Console.WriteLine("1. Log in as Admin");
            Console.WriteLine("2. Log in as Customer");
            Console.WriteLine("3. Register New Customer");
            Console.WriteLine("4. Exit");

            string? choice = (Console.ReadLine());

            if (choice == "1")
            {
          //      admin.AdminMenu();
                continue;
            }
            else if (choice == "2")
            {

            //    user.Login();
                continue;
            }
            else if (choice == "3")
            {
              //  user.RegisterUser();
                continue;
            }
            else if (choice == "4")
            {
                break;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Pick something valid");
                continue;
            }
        }
    }
}