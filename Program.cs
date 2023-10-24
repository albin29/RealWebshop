using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace Webshop;
public class Program
{
    public static void Main(string[] args)
    {
        User user = new User();
        Admin admin = new Admin(user);
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the store system!\n");
            Console.WriteLine("1 - Log in as admin");
            Console.WriteLine("2 - Log in as a customer");
            Console.WriteLine("3 - Register as a new customer\n");
            Console.WriteLine("0 - Exit\n");

            string? choice = (Console.ReadLine());

            if (choice == "1")
            {
                admin.AdminMenu();
                continue;
            }
            else if (choice == "2")
            {
                user.Login();
                continue;
            }
            else if (choice == "3")
            {
                user.RegisterUser();
                continue;
            }
            else if (choice == "0")
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
