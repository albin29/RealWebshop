using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace Webshop;
public class Program
{

    static void Main(string[] args)
    {
        LoginMenu loginMenu = new LoginMenu();
        IUser? user = null;

        do
        {
            switch (Console.ReadLine())
            {
                case "1":

                    loginMenu.RegisterUser();
                    break;

                case "2":
                    user = loginMenu.Login();
                    break;
            }
        } while (user is null);

        user.DisplayMainMenu();
    }
}
