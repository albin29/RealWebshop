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

            Console.Write("Please enter a unique username of your choice: ");

            string userinputname = Console.ReadLine();
            if (UsernameExists(userinputname))
            {
                Console.Clear();
                Console.WriteLine("Username already exists");
                continue;
            }
            while (true)
            {
                Console.WriteLine(); 
                Console.Write("Please choose a password: ");
                passwordinput = Console.ReadLine();

                if (passwordinput == "")
                {
                    Console.WriteLine("No empty password allowed");
                    continue;
                }
                break;
            }
            Console.Clear();

            Console.WriteLine(userinputname + ", has sucessfully been registered as a new customer!\n");

            loginlistUser.Add(userinputname, passwordinput);
            // Adds the user to the CSV file
            //(CHANGE) deleted the streamwriter that Manuel doesnt like AND also changed ; seperator to , seperator

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
            string? userinputname = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Please enter your password: ");
            string? passwordinput = Console.ReadLine();
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
                Console.WriteLine("Your credentials do not exist.");
                Console.WriteLine("Please try again!");
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
            Console.WriteLine("1 - to make a purchase");
            Console.WriteLine("2 - to view  your purchase history");
            Console.WriteLine("3 - to view your cart");
            Console.WriteLine("4 - to checkout");
            Console.WriteLine("x - to exit\n");
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

                Console.ReadKey();
                Console.Clear();
            }
            else if (userChoice == "3")
            {
                Console.Clear();
                Console.WriteLine("Cart contains: \n");
                for (int i = 0; i < shoppingList.Count; i++)
                {
                    Console.WriteLine(shoppingList[i].Name + " " + shoppingList[i].Price);
                }
                Console.ReadKey();
                Console.Clear();
            }
            else if (userChoice == "4")// (CHANGE) added , instead of ; seperator and removed streamwriter + mad sure cart is empty after purchase
            {
                float totalAmount = 0;
                {
                    for (int i = 0; i < shoppingList.Count; i++)
                    {
                        totalAmount += shoppingList[i].Price;
                        File.AppendAllText("../../../buyHistory.csv", $"{realusername},{shoppingList[i].Name},{shoppingList[i].Price},{DateTime.Now}\n");
                    }
                }
                shoppingList.Clear();

                Console.Clear();
                Console.WriteLine("Your purchase was successful! Total amount paid: " + totalAmount + "$");
                continue;
            }

            else if (userChoice=="x") {


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

