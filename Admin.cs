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
using System;

namespace Webshop;

public class Admin
{
    public string username = "admin";
    string adminpassword = "123";

    User user = new User();
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
                Console.Clear();
                Console.WriteLine("The password was wrong.");
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

            Console.Write("Please navigate by entering the proceeding character: ");

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
    public void AddProduct()
    {
        Console.Clear();
        Console.WriteLine("Add a new product\n");
        Console.Write("Please enter a unique product name: ");
        string? productName = Console.ReadLine();
        Console.WriteLine();
        Console.Write("Please enter a product price in $ per unit: ");
        string? productPrice = Console.ReadLine();

        Product product = new Product(float.Parse(productPrice), productName);
        float price = product.Price;
        string name = product.Name;

        while (true)
        {
            Products.RegisterProduct(product); //add a product
            Console.Clear();
            Console.WriteLine("You successfully added '" + productName + "' at " + productPrice + " $ per unit to your product list.\n");
            Console.WriteLine("What would you like to do next?\n");
            Console.WriteLine("1 - to add another product.");
            Console.WriteLine("2 - to view product list.");
            Console.WriteLine("m - to go back to your main menu.");
            Console.WriteLine("x - to log out.\n");

            Console.Write("Please navigate by entering the preceding character: ");
            string? menuselection = Console.ReadLine();

            if (menuselection == "1")
            {
                AddProduct();
            }
            else if (menuselection == "2")
            {
                Products.ListProducts();
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
    public void EditProduct()
    {
        while (true)
        {
            Products.ListProducts();
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

    /* public void EditProduct()

     {
         while (true)
         {
             Console.Clear();
             Console.WriteLine("Edit Product\n");

             Products.WriteProducts();


                 /*string lines = "";
                 foreach (var product in productList)
                 {
                     lines += product.Name + ";" + product.Price + "\n";
                 }
                 File.WriteAllText("../../../products.csv", lines);


             string[] lines = File.ReadAllLines("../../../products.csv");

             foreach (string line in lines)
             {
                 Console.WriteLine(line); //displays product list
             }

             Console.WriteLine();
             Console.WriteLine("Which product would you like to edit?\n");
             Console.Write("Please type in the product name: ");
             string? productToEdit = Console.ReadLine();

             if (productToEdit == "m")
             {
                 {
                     Console.WriteLine("Code needed to edit product.");
                 }
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
     }//some code missing!*/
    public void DeleteProduct()

    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Delete Product\n");


            string[] lines = File.ReadAllLines("../../../products.csv");

            foreach (string line in lines)
            {
                Console.WriteLine(line); //displays product list
            }


            Console.WriteLine("Which product would you like to delete?\n");
            Console.Write("Please type in the product name: ");
            string? productToEdit = Console.ReadLine();

            if (productToEdit == "M")
            {
                Console.WriteLine("Code needed to delete product.");   //code needed to delete product!
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
    }//some code missing
    public void UserMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Manage users \n");
            Console.WriteLine("1 - to add a new user.");
            Console.WriteLine("2 - to edit an existing user.");
            Console.WriteLine("3 - to delete a user.");
            Console.WriteLine("m - to go back to your main menu.");
            Console.WriteLine("x - to log out.\n");
            Console.Write("Please navigate by entering the preceding character: ");
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
                {
                    //Main();
                    break;
                }

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

            Console.WriteLine("What would your like to do next?\n");

            Console.WriteLine("m - to go back to your main menu.");
            Console.WriteLine("x - to log out.\n");

            Console.Write("Please navigate by entering the preceding character: ");
            string? menuselection = Console.ReadLine();

            if (menuselection == "m")
            {
                AdminMainMenu();
            }

            else if (menuselection == "x")
            {
                {
                    //Main();
                }
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
            Console.WriteLine("Edit User\n");


            string[] lines = File.ReadAllLines("../../../users.csv");

            foreach (string line in lines)
            {
                Console.WriteLine(line); //displays user list
            }


            Console.WriteLine("Which user would you like to edit?");

            Console.Write("Please type in the user name: ");
            string? productToEdit = Console.ReadLine();

            if (productToEdit == "M")
            {
                {
                    Console.WriteLine("Code needed to edit user.");
                }
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
    public void DeleteUser()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Delete User\n");


            string[] lines = File.ReadAllLines("../../../users.csv");

            foreach (string line in lines)
            {
                Console.WriteLine(line); //displays user list
            }


            Console.WriteLine("Which user would you like to delete?\n");

            Console.Write("Please type in the user name: ");
            string? productToEdit = Console.ReadLine();

            if (productToEdit == "M")
            {
                {
                    Console.WriteLine("Code needed to delete user.");   //code needed HERE to delete user!
                }
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
    } //some code missing
    public void DisplayBuyHistory()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Order and transaction history\n");

            History history = new History();
            history.viewAllBuyHistory();
           
            Console.WriteLine();
            Console.WriteLine("What would your like to do next?\n");

            Console.WriteLine("m - to go back to your main menu.");
            Console.WriteLine("x - to log out.\n");

            Console.Write("Please navigate by entering the preceding character: ");
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
                Console.WriteLine("Invalid entry.");
                Console.WriteLine("Press any key to try again!");
                string? anykey = Console.ReadLine();
                continue;
            }
        }
    }
}