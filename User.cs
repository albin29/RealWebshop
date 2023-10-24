using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Webshop;

public class User
{
    // important stuff
    History history = new History();
    string? realusername;
    public Dictionary<string, string> loginlistUser = new Dictionary<string, string>();
    List<Product> shoppingList = new List<Product>();

    // function to check if username already exists
    public bool UsernameExists(string username)
    {
        if (loginlistUser.ContainsKey(username))
        {
            return true;
        }
        return false;
    }

    // Function for registering a new user
    public bool RegisterUser()
    {
        string? passwordinput;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Register as a new user\n");
            Console.Write("Please enter a username of your choice or press 0 to go back:\n");
            string userinputname = Console.ReadLine();
            if (userinputname == "0")
            {
                break;
            }
            if (UsernameExists(userinputname))
            {
                Console.Clear();
                Console.WriteLine("Username already exists.");
                continue;
            }
            while (true)
            {
                Console.Write("\nPlease choose a password: ");
                passwordinput = Console.ReadLine();

                if (passwordinput == "")
                {
                    Console.WriteLine("No empty password");
                    continue;
                }
                break;
            }
            Console.Clear();
            Console.WriteLine(userinputname + " has sucessfully been registered as a new user.\n");
            loginlistUser.Add(userinputname, passwordinput);
            // Adds the user to the CSV file
            File.AppendAllText("../../../users.csv", $"{userinputname},{passwordinput}\n");
            break;
        }
        return true;
    }
    // Dictionary csv for user login and password
    public User()
    {
        string[] filen = File.ReadAllLines("../../../users.csv");

        foreach (string line in filen)
        {
            if (line == "")
            {
                continue;
            }

            filen = line.Split(',');
            string name2 = filen[0];
            string password2 = filen[1];
            loginlistUser.Add(name2, password2);
        }
    }
    // Login function for user
    public bool Login()
    {
        while (true)
        {
            Console.Clear();
            Console.Write("Please enter your username: ");
            string userinputname = Console.ReadLine();
            Console.Write("\nPlease enter your password: ");
            string passwordinput = Console.ReadLine();
            if (loginlistUser.ContainsKey(userinputname) && passwordinput == loginlistUser[userinputname])
            {
                realusername = userinputname;
                Console.WriteLine(userinputname + " has logged in");
                Console.Clear();
                UserLoggedin();
                break;
            }
            else
            {
                Console.WriteLine("\nIncorrect user or password.\n");
                Console.WriteLine("Please try again");
                Console.ReadKey();
            }
        }
        return true;
    }
    // Menu presented to user when logged in
    public void UserLoggedin()
    {
        while (true)
        {
            Console.WriteLine("What do you want to do?\n");
            Console.WriteLine("1 - Fill cart");
            Console.WriteLine("2 - View purchase history");
            Console.WriteLine("3 - View cart");
            Console.WriteLine("4 - Checkout\n");
            Console.WriteLine("0 - Go back\n");
            Console.Write("Please navigate by entering the respective character: ");
            string userChoice = Console.ReadLine();
            if (userChoice == "1")
            {
                Console.Clear();
                Products.ShopItems(shoppingList);
            }
            else if (userChoice == "2")
            {
                Console.Clear();
                Console.WriteLine(realusername + " Buyhistory:\n");
                history.ViewBuyHistory(realusername);
                Console.WriteLine("\nPress enter to proceed.");
                Console.ReadKey();
                Console.Clear();
            }
            else if (userChoice == "3")
            {
                Console.Clear();
                Console.WriteLine("Cart contains: \n");
                for (int i = 0; i < shoppingList.Count; i++)
                {
                    Console.WriteLine($"{shoppingList[i].Name} {shoppingList[i].Price}");
                }
                Console.WriteLine("\nPress enter to proceed.");
                Console.ReadKey();
                Console.Clear();
            }
            else if (userChoice == "4")
            {
                if (shoppingList.Count < 1)
                {
                    Console.Clear();
                    Console.WriteLine("Your cart is empty, please input a product ");
                    Console.ReadKey();
                    continue;
                }
                float totalAmount = 0;
                string totalHistory = "";
                foreach (Product product in shoppingList)
                {
                    totalAmount += product.Price;
                    totalHistory += ($"{realusername},{product.Name},{product.Price},{DateTime.Now}\n");
                }
                File.AppendAllText("../../../buyHistory.csv", totalHistory);
                shoppingList.Clear();
                Console.Clear();
                Console.WriteLine("Your purchase was successful! Total amount paid: " + totalAmount + "$");
                Console.ReadKey();
                Console.Clear();
                continue;
            }
            else if (userChoice == "0")
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

