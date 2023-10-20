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
        Console.WriteLine("Admin Login");
        while (true)
        {
            Console.Write("Please enter your admin password: ");
            string? enteredpassword = Console.ReadLine();

            if (enteredpassword == adminpassword)
            {
                AdminMainMenu();
                break;
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
            Console.WriteLine("Hello, Admin! What would you like to do?");
            Console.WriteLine("1 - to manage products.");
            Console.WriteLine("2 - to manage users.");
            Console.WriteLine("3 - to view order and transaction history.");
            Console.WriteLine("x - to log out.");
            Console.Write("Please navigate by entering the preceding character: ");
            string? menuselection = Console.ReadLine();

            if (menuselection == "1")
            {
                ProductMenu();
                break;
            }
            else if (menuselection == "2")
            {
                UserMenu();
                break;
            }
            else if (menuselection == "3")
            {
                DisplayBuyHistory();
                break;
            }
            else if (menuselection == "x")
            {
                break;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid entry.");
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
            Console.WriteLine("Manage products");
            Console.WriteLine("1 - to add a new product.");
            Console.WriteLine("2 - to edit an existing product.");
            Console.WriteLine("3 - to delete a product.");
            Console.WriteLine("m - to go back to your main menu.");
            Console.WriteLine("x - to log out.");
            Console.Write("Please navigate by entering the proceeding character: ");
            string? menuselection = Console.ReadLine();
            if (menuselection == "1")
            {
                AddProduct();
                break;
            }
            else if (menuselection == "2")
            {
                EditProduct();
                break;
            }

            else if (menuselection == "3")
            {
                DeleteProduct();
                break;
            }
            else if (menuselection == "m")
            {
                AdminMainMenu();
                break;
            }
            else if (menuselection == "x")
            {
                break;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid entry.");

                Console.WriteLine("Press any key to try again!");
                string? anykey = Console.ReadLine();
                continue;
            }
        }
    }
    public void EditProduct()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Edit Product");
            Console.WriteLine("Current Products:");
            for (int i = 0; i < Products.productList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Products.productList[i].Name} - {Products.productList[i].Price} SEK");
            }
            Console.WriteLine("\nWhich product would you like to edit (enter the product number or 0 to go back to the menu)?");
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

                Console.WriteLine($"You selected: {Products.productList[index].Name}");
                Console.Write("Enter the new price: ");

                if (float.TryParse(Console.ReadLine(), out float newPrice))
                {
                    // Create a new Product with the updated price
                    Products.productList[index] = new Product(newPrice, Products.productList[index].Name);
                    Products.WriteProducts(); // Save the updated product list to the CSV file
                    Console.Clear();
                    Console.WriteLine("Product price updated successfully!");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid price. Please enter a valid price.");
                    Console.WriteLine("Press any key to try again.");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid entry. Please enter a valid product number.");
                Console.WriteLine("Press any key to try again.");
                Console.ReadKey();
            }
        }
    }

    private void AddProduct()
    {

        Console.Clear();
        Console.WriteLine("Add a new product");
        Console.WriteLine("Product List"); //Productlist display
        Console.Write("Please enter a unique product name: ");
        string? productName = Console.ReadLine();
        while (true)
        {
            Console.WriteLine("Please enter a product price in Decimals with comma or whole numbers per unit: ");
            string? productPriceInput = Console.ReadLine();

            if (!float.TryParse(productPriceInput, out float productPrice))
            {
                Console.Clear();
                Console.WriteLine("That was not a valid value");
                continue;
            }
            else
            {
                Product product = new Product(productPrice, productName);
                float price = product.Price;
                string name = product.Name;
                Products.RegisterProduct(product);
                Console.WriteLine("Product added successfully.");
                break;
            }
        }

        while (true)
        {
            Console.Clear();
            Console.WriteLine("You successfully added products");
            Console.WriteLine("What would you like to do next?");
            Console.WriteLine("1 - to add another product.");
            Console.WriteLine("2 - to view product list.");
            Console.WriteLine("m - to go back to your main menu.");
            Console.WriteLine("x - to log out.");
            Console.Write("Please navigate by entering the preceding character: ");
            string? menuselection = Console.ReadLine();

            if (menuselection == "1")
            {
                AddProduct();
                break;
            }
            if (menuselection == "2")
            {
                Console.WriteLine("Display Productlist."); //code missing display updated product list
                break;
            }
            if (menuselection == "m")
            {
                AdminMainMenu();
                break;
            }
            if (menuselection == "x")
            {
                break; // AdminMenu();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid entry.");
                Console.WriteLine("Press any key to try again!");
                string? anykey = Console.ReadLine();
                continue;
            }

        }
    }
    public void DeleteProduct()
    {
        while (true)
        {
            while (true)
            {
                Console.WriteLine("You are deleting a product.");
                Console.WriteLine("Here's a list of all products.");

                int n = 0;
                foreach (var product in Products.productList)
                {
                    Console.WriteLine((++n).ToString() + ": " + product.Name + ", " + product.Price);
                }

                Console.WriteLine("Enter a product's number to remove that product.");
                Console.WriteLine("Enter 0 to exit this mode.");
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
            Console.WriteLine("Manage users");
            Console.WriteLine("1 - to add a new user.");
            Console.WriteLine("2 - to edit an existing user.");
            Console.WriteLine("3 - to delete a user.");
            Console.WriteLine("M - to go back to your main menu.");
            Console.WriteLine("X - to log out.");
            Console.Write("Please navigate by entering the preceding character: ");
            string? menuselection = Console.ReadLine();

            if (menuselection == "1")
            {

                AddUser();

                break;
            }

            if (menuselection == "2")
            {

                EditUser();

                break;
            }

            if (menuselection == "3")
            {

                DeleteUser();

                break;
            }

            else if (menuselection == "m")
            {

                AdminMainMenu();

                break;
            }

            else if (menuselection == "x")
            {
                break;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid entry.");
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
            Console.WriteLine("What would your like to do next?");
            Console.WriteLine("m - to go back to your main menu.");
            Console.WriteLine("x - to log out.");
            Console.Write("Please navigate by entering the preceding character: ");
            string? menuselection = Console.ReadLine();
            if (menuselection == "m")
            {
                AdminMainMenu();
                break;
            }

            else if (menuselection == "x")
            {
                break;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid entry.");
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
            Console.WriteLine("Edit User");
            string[] lines = File.ReadAllLines("../../../users.csv");

            foreach (string line in lines)
            {
                Console.WriteLine(line); //displays user list
            }
            Console.WriteLine("Which user would you like to edit?");
            Console.Write("Please type in the user name: ");
            string? userToEdit = Console.ReadLine();

            if (user.loginlistUser.ContainsKey(userToEdit))
            {
                    Console.WriteLine("Enter new password");
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
                Console.WriteLine("Invalid entry.");
                Console.WriteLine("Enter existing account");
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
            Console.WriteLine("Delete User");
            string[] lines = File.ReadAllLines("../../../users.csv");

            foreach (string line in lines)
            {
                Console.WriteLine(line); //displays user list
            }
            Console.WriteLine("Which user would you like to delete?");

            Console.Write("Please type in the user name: ");
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
                Console.WriteLine("Invalid entry.");
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
            Console.WriteLine("Order and transaction history");
            string[] lines = File.ReadAllLines("../../../buyHistory.csv");
            foreach (string line in lines)
            {
                Console.WriteLine(line); //displays purchase history
            }

            Console.WriteLine("What would your like to do next?");
            Console.WriteLine("M - to go back to your main menu.");
            Console.WriteLine("X - to log out.");
            Console.Write("Please navigate by entering the preceding character: ");
            string? menuselection = Console.ReadLine();
            if (menuselection == "m")
            {
                AdminMainMenu();
                break;
            }

            else if (menuselection == "x")
            {
                break;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid entry.");
                Console.WriteLine("Press any key to try again!");
                string? anykey = Console.ReadLine();
                continue;
            }
        }
    }
}