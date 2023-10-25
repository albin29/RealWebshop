using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace Webshop;
public class Program
{
    static void Main(string[] args)
    {
        IUser? user = null;
        do
        {
        Console.WriteLine("Welcome!\n\n1 / Register \n2 / Login");

            switch (Console.ReadLine())
            {
                case "1":

                    LoginMenu.RegisterUser();
                    Console.WriteLine("You have successfully created a user!");
                    Console.ReadKey();
                    Console.Clear();

                    continue;

                case "2":
                    user = LoginMenu.LoginUser();
                    break;
                default:
                    Console.WriteLine("Please enter 1 or 2");
                    Thread.Sleep(2000);
                    Console.Clear();
                    continue;
            }
        } while (user is null);

        user.MainMenu();
    }
}
