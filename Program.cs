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
            Console.WriteLine("1 - to log in as admin");
            Console.WriteLine("2 - to log in as a customer");
            Console.WriteLine("3 - to register as a new customer");
            Console.WriteLine("x - to exit\n");
            Console.Write("Please navigate by entering the respective character: ");

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
            else if (choice == "x")
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
