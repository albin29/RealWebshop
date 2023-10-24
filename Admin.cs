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
            Console.WriteLine("1 - Manage products.");
            Console.WriteLine("2 - Manage users.");
            Console.WriteLine("3 - View order and transaction history.\n");
            Console.WriteLine("0 - Log out.\n");
            string? menuselection = Console.ReadLine();

            if (menuselection == "1")
            {
                ProductMenu();
                continue;
            }
            else if (menuselection == "2")
            {
                UserMenu();
                continue;
            }
            else if (menuselection == "3")
            {
                DisplayBuyHistory();
                continue;
            }
            else if (menuselection == "0")
            {
                AdminMenu();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid entry.\n");
                Console.WriteLine("Press any key to try again!");
                Console.ReadKey();
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
            Console.WriteLine("1 - Add a new product.");
            Console.WriteLine("2 - Edit an existing product.");
            Console.WriteLine("3 - Delete a product.\n");
            Console.WriteLine("0 - Go back");
            string? menuselection = Console.ReadLine();
            if (menuselection == "1")
            {
                AddProduct();
                continue;
            }
            else if (menuselection == "2")
            {
                EditProduct();
                continue;
            }

            else if (menuselection == "3")
            {
                DeleteProduct();
                continue;
            }
            else if (menuselection == "0")
            {
                break;
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
    public void AddProduct()
    {
        Console.Clear();
        Console.Write("Please enter a unique product name or enter 0 to go back: \n");

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
            Console.Write("Please enter the product number or press 0 to go back: ");

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
            Console.Clear();
            Console.WriteLine("Delete products\n");
            Console.WriteLine("Products:\n");

            DisplayProductlist();

            Console.Write("\nPlease enter the product number or 0 to go back:\n");
            string id = Console.ReadLine();
            if (id.Length == 0)
            {

                Console.WriteLine("You have failed to enter a product number.");
                continue;
            }
            if (id == "0")
            {
                break;
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
    public void UserMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Manage users\n");
            Console.WriteLine("1 - Add a new user.");
            Console.WriteLine("2 - Edit an existing user.");
            Console.WriteLine("3 - Delete a user.\n");
            Console.WriteLine("0 - Got back");
            string? menuselection = Console.ReadLine();

            if (menuselection == "1")
            {
                AddUser();
                continue;
            }
            if (menuselection == "2")
            {
                EditUser();
                continue;
            }
            if (menuselection == "3")
            {
                DeleteUser();
                continue;
            }
            else if (menuselection == "0")
            {
                break;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid entry.\n");
                Console.WriteLine("Press any key to try again!");
                Console.ReadKey();
                continue;
            }
        }
    }
    public void AddUser()
    {
        user.RegisterUser(); // Accessing the public method
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
            Console.Write("Please type in the user name or press 0 to back:\n");
            string? userToEdit = Console.ReadLine();

            if (userToEdit == "0")
            {
                break;
            }
            if (user.loginlistUser.ContainsKey(userToEdit))
            {
                Console.Write("\nChoose a new password:");
                string? password = Console.ReadLine();
                user.loginlistUser[userToEdit] = password;
                string csvPaste = "";
                foreach (string piece in lines)
                {
                    string[] info = piece.Split(',');
                    if (info[0] == userToEdit)
                    {
                        csvPaste += ($"{info[0]},{password}\n");
                        continue;
                    }
                    csvPaste += ($"{piece}\n");
                }
                // Write the content to the file, replacing any existing content
                File.WriteAllText("../../../users.csv", csvPaste);
                Console.Clear();
                Console.WriteLine($"Success! You have changed {userToEdit} password");
                Console.ReadKey();
                break;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid entry.\n");
                Console.WriteLine("Enter an existing username");
                Console.ReadKey();
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

            Console.Write("Please type in the user name or enter 0 to exit:\n");
            string? userToEdit = Console.ReadLine();
            if (userToEdit == "0")
            {
                break;
            }
            if (user.loginlistUser.ContainsKey(userToEdit))
            {
                user.loginlistUser.Remove(userToEdit);
                string csvPaste = "";
                foreach (string piece in lines)
                {
                    string[] info = piece.Split(',');
                    if (info[0] == userToEdit)
                    {
                        continue;
                    }
                    csvPaste += ($"{piece}\n");
                }
                Console.Clear();
                Console.WriteLine($"Success! You have deleted {userToEdit}");
                Console.ReadKey();
                // Write the content to the file, replacing any existing content
                File.WriteAllText("../../../users.csv", csvPaste);
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

            Console.WriteLine("\nPress any key to go back\n");
            Console.ReadKey();
            break;
        }
    }
}
