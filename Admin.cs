using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;
using Microsoft.Win32;

namespace Webshop;
public record Admin(string name) : IUser
{
    public void MainMenu()
    {
        bool quit = false;
        while (quit = false)
        {
            Console.Clear();
            Console.WriteLine("|||||||||||||\nWelcome to your domicile\n|||||||||||");
            Console.WriteLine("1 / Edit customer\n2 / Edit product\n3 / View all purchase history\n9 / Go back\n");
            string? menuChoice = Console.ReadLine();
            switch (menuChoice)
            {
                case "1":
                    break;
                case "2":
                    break;
                case "3":
                    break;
                case "9":
                    quit = true;
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Please enter one of the shown numbers");
                    Console.ReadKey();
                    break;
            }
        }
    }
}