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
using System.Xml.Linq;
using System.Formats.Asn1;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using Microsoft.VisualBasic;
using System.Diagnostics.Metrics;

namespace Webshop;

public class Admin
{
    public string username = "admin";
    string adminpassword = "123";

    User user;

    public Admin(User user2)
    {

        user = user2;
    }
    public void AdminMenu()
    {
        Console.Clear();
        Console.WriteLine("Admin Login\n");
        while (true)
        {
            Console.Write("Please enter your admin password OR '2' to switch to User Login: ");
            string? enteredpassword = Console.ReadLine();

            if (enteredpassword == adminpassword)
            {
                AdminMainMenu();
            }

            else if (enteredpassword == "2")
            {
                user.Login();
            }

            else
            {
                Console.WriteLine("The password was wrong");
                continue;
            }
        }
    }
    public void AdminMainMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Hello, Admin! What would you like to do?\n");
            Console.WriteLine("1 - to manage products.");
            Console.WriteLine("2 - to manage users.");
            Console.WriteLine("3 - to view order and transaction history.");
            Console.WriteLine("x - to log out.\n");
            Console.Write("Please navigate by entering the preceding character: ");
            string? menuselection = Console.ReadLine();

            if (menuselection == "1")
            {
                ProductMenu();
            }
            else if (menuselection == "2")
            {
                UserMenu();
            }
            else if (menuselection == "3")
            {
                DisplayBuyHistory();
            }
            else if (menuselection == "x")
            {
                AdminMenu();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid entry.\n");
                Console.WriteLine("Press any key to try again!");
                string? anykey = Console.ReadLine();
                continue;
            }
        }
    }
    public void ProductMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Manage products\n");
            Console.WriteLine("1 - to add a new product.");
            Console.WriteLine("2 - to edit an existing product.");
            Console.WriteLine("3 - to delete a product.");
            Console.WriteLine("m - to go back to your main menu.");
            Console.WriteLine("x - to log out.\n");
            Console.Write("Please navigate by entering the respective character: ");
            string? menuselection = Console.ReadLine();
            if (menuselection == "1")
            {
                AddProduct();
            }
            else if (menuselection == "2")
            {
                EditProduct();
            }

            else if (menuselection == "3")
            {
                DeleteProduct();
            }
            else if (menuselection == "m")
            {
                AdminMainMenu();
            }
            else if (menuselection == "x")
            {
                AdminMenu();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid entry.\n");

                Console.WriteLine("Press any key to try again!");
                string? anykey = Console.ReadLine();
                continue;
            }
        }
    }
    private void AddProduct()
    {
        Console.Clear();
        Console.Write("Please enter a unique product name or enter '0' to go back: \n");

        string? productName = Console.ReadLine();
        while (true)
        {
            if (productName == "0")
            {
                Console.Clear();
                break;
            }
            Console.Write("\nPlease enter a product price in $ per unit: ");
            string? productPriceInput = Console.ReadLine();

            if (!float.TryParse(productPriceInput, out float productPrice))
            {
                Console.Clear();
                Console.WriteLine("This was not a valid entry. Make sure you are entering a number.");
                continue;
            }
            else
            {
                Product product = new Product(productPrice, productName);
                float price = product.Price;
                string name = product.Name;
                Products.RegisterProduct(product);
                Console.Clear();
                Console.WriteLine("You successfully added " + productName + " at " + productPrice + " $ per unit to your product list.\n");
                Console.WriteLine("Enter to go back and continue");
                Console.ReadLine();
                break;

            }
        }

        while (true)
        {
            Console.Clear();
            Console.WriteLine("What would you like to do next?\n");
            Console.WriteLine("1 - to add another product.");
            Console.WriteLine("2 - to view the updated product list.");
            Console.WriteLine("m - to go back to your main menu.");
            Console.WriteLine("x - to log out.\n");
            Console.Write("Please navigate by enterning the respective character: ");
            string? menuselection = Console.ReadLine();

            if (menuselection == "1")
            {
                AddProduct();
                break;
            }
            if (menuselection == "2")
            {
                Console.Clear();
                Console.WriteLine("Products:\n");

                DisplayProductlist();
                /* (int i = 0; i < Products.productList.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {Products.productList[i].Name} - {Products.productList[i].Price} $");
                }*/
                Console.WriteLine("\nEnter to continue.");
                Console.ReadLine();

                break;

            }
            if (menuselection == "m")
            {
                AdminMainMenu();
            }
            if (menuselection == "x")
            {
                AdminMenu();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid entry.\n");
                Console.WriteLine("Press any key to try again!");
                string? anykey = Console.ReadLine();
                continue;
            }

        }
    }
    public void DisplayProductlist()
    {
        for (int i = 0; i < Products.productList.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Products.productList[i].Name} - {Products.productList[i].Price} $");
        }
    }
    public void EditProduct()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Edit product\n");

            DisplayProductlist(); //View Productlist

            Console.WriteLine("\nWhich product would you like to edit?\n");
            Console.Write("Please enter the product number or ´0´ to go back: ");

            string userChoice = Console.ReadLine();

            if (userChoice == "0")
            {
                Console.Clear();
                break;
            }
            if (int.TryParse(userChoice, out int productNumber) && productNumber >= 1 && productNumber <= Products.productList.Count)
            {
                Console.Clear();
                int index = productNumber - 1; // Adjust for 0-based indexing

                Console.WriteLine($"You selected: '{Products.productList[index].Name}', price: ${Products.productList[index].Price} ");
                Console.Write("\nPlease enter new price: ");

                if (float.TryParse(Console.ReadLine(), out float newPrice))
                {
                    // Create a new Product with the updated price
                    Products.productList[index] = new Product(newPrice, Products.productList[index].Name);
                    Products.WriteProducts(); // Save the updated product list to the CSV file
                    Console.Clear();
                    Console.WriteLine("Product price updated successfully!\n");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid price. Please enter a valid price.\n");
                    Console.WriteLine("Press any key to try again.");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid entry. Please enter a valid product number.\n");
                Console.WriteLine("Press any key to try again.");
                Console.ReadKey();
            }
        }
    }
    public void DeleteProduct()
    {
        while (true)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Delete products\n");
                Console.WriteLine("Products:\n");

                DisplayProductlist();

                Console.WriteLine("\nWhich product would you like to delete?\n");
                Console.Write("Please enter the product number or ´0´ to go back: ");
                string id = Console.ReadLine();
                if (id.Length == 0)
                {

                    Console.WriteLine("You have successfully failed to enter a product number.");
                    continue;
                }

                if (id == "0")
                {
                    return;
                }

                int intId;
                if (int.TryParse(id, out intId))
                {
                    Product product = Products.productList[(intId - 1)];
                    Products.UnregisterProduct(product);
                    Console.WriteLine("You have successfully removed " + product.Name + "!");
                }
            }
        }
    }
    public void UserMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Manage users\n");
            Console.WriteLine("1 - to add a new user.");
            Console.WriteLine("2 - to edit an existing user.");
            Console.WriteLine("3 - to delete a user.");
            Console.WriteLine("m - to go back to your main menu.");
            Console.WriteLine("x - to log out.\n");
            Console.Write("Please navigate by entering the respective character: ");
            string? menuselection = Console.ReadLine();

            if (menuselection == "1")
            {

                AddUser();

            }

            if (menuselection == "2")
            {

                EditUser();

            }

            if (menuselection == "3")
            {

                DeleteUser();
            }

            else if (menuselection == "m")
            {

                AdminMainMenu();

            }

            else if (menuselection == "x")
            {
                AdminMenu();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid entry.\n");
                Console.WriteLine("Press any key to try again!");
                string? anykey = Console.ReadLine();
                continue;
            }
        }
    }
    public void AddUser()
    {
        user.RegisterUser(); // Accessing the public method
        while (true)
        {
            Console.WriteLine("What would your like to do next?\n");
            Console.WriteLine("m - to go back to your main menu.");
            Console.WriteLine("x - to log out.\n");
            Console.Write("Please navigate by entering the respective character: ");
            string? menuselection = Console.ReadLine();
            if (menuselection == "m")
            {
                AdminMainMenu();
            }

            else if (menuselection == "x")
            {
                AdminMenu();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid entry.\n");
                Console.WriteLine("Press any key to try again!");
                string? anykey = Console.ReadLine();
                continue;
            }
        }
    }
    public void EditUser() //some code missing
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Edit User\n");
            string[] lines = File.ReadAllLines("../../../users.csv");

            foreach (string line in lines)
            {
                Console.WriteLine(line); //displays user list
            }
            Console.WriteLine("\nWhich user would you like to edit?\n");
            Console.Write("Please type in the user name: ");
            string? userToEdit = Console.ReadLine();

            if (user.loginlistUser.ContainsKey(userToEdit))
            {
                Console.Write("\nChoose a new password:");
                string? password = Console.ReadLine();
                user.loginlistUser[userToEdit] = password;
                string csvContent = string.Join(Environment.NewLine, user.loginlistUser.Select(entry => $"{entry.Key},{entry.Value}"));
                // Write the content to the file, replacing any existing content
                File.WriteAllText("../../../users.csv", csvContent);
                break;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid entry.\n");
                Console.WriteLine("Enter an existing username");
                string? anykey = Console.ReadLine();
                continue;
            }
        }
    }
    public void DeleteUser()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Delete user\n");
            string[] lines = File.ReadAllLines("../../../users.csv");

            foreach (string line in lines)
            {
                Console.WriteLine(line); //displays user list
            }
            Console.WriteLine("\nWhich user would you like to delete?\n");

            Console.Write("Please type in the user name or press enter to exit: ");
            string? userToEdit = Console.ReadLine();

            if (user.loginlistUser.ContainsKey(userToEdit))
            {
                user.loginlistUser.Remove(userToEdit);
                string csvContent = string.Join(Environment.NewLine, user.loginlistUser.Select(entry => $"{entry.Key},{entry.Value}"));
                // Write the content to the file, replacing any existing content
                File.WriteAllText("../../../users.csv", csvContent);
                break;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid entry.\n");
                Console.WriteLine("Press any key to try again! Make sure to use correct spelling.");
                string? anykey = Console.ReadLine();
                continue;
            }
        }
    }
    public void DisplayBuyHistory()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Order and transaction history\n");

            History history = new History();
            history.viewAllBuyHistory();
            
            Console.WriteLine("\nWhat would your like to do next?\n");
            Console.WriteLine("m - to go back to your main menu.");
            Console.WriteLine("x - to log out.\n");
            Console.Write("Please navigate by entering the respective character: ");
            string? menuselection = Console.ReadLine();
            if (menuselection == "m")
            {
                AdminMainMenu();
            }

            else if (menuselection == "x")
            {
                AdminMenu();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid entry.\n");
                Console.WriteLine("Press any key to try again!");
                string? anykey = Console.ReadLine();
                continue;
            }
        }
    }
}
