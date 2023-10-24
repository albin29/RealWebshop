using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace Webshop;
public record Customer(string name, List<Product> Cart) : IUser
{

    public void MainMenu()
    {
        bool quit = false;
        while (!quit)
        {
            Console.Clear();
            Console.WriteLine("============================\nWelcome to the customer menu\n============================");
            Console.WriteLine("1 / Add products to cart\n2 / Check your cart\n3 / Check your buyhistory\n\n9 / Leave menu");
            string? menuChoice = Console.ReadLine();

            switch (menuChoice)
            {
                case "1":
                    AddProductToCart();
                    break;
                case "2":
                    CheckCart();
                    break;
                case "3":
                    CheckUserPurchaseHistory();
                    break;
                case "9":
                    quit = true;
                    break;

            }

        }
    }
    public void AddProductToCart()
    {
        string[] products = File.ReadAllLines("../../../products.csv");
        Console.Clear();
        Console.WriteLine("Enter the number next to the product you wish to add to your cart.\n");
        int placement = 1;

        foreach (var product in products)
        {
            string[] detail = product.Split(',');

            Console.WriteLine($"{placement}. {detail[0]} : {detail[1]}$");
            placement++;
        }
        Console.WriteLine("\nInput 0 to go back to menu");
        string? choice = Console.ReadLine();

        if (int.TryParse(choice, out int number))
        {
            if (number == 0)
            {
                return;
            }
            else if (number > 0 && number <= products.Length)
            {
                string chosenData = products[number - 1];
                Product chosenProduct = new Product(chosenData);
                Cart.Add(chosenProduct);
            }
            else
            {
                Console.WriteLine("Sorry, input doesnt match any product");
                Console.ReadKey();
                AddProductToCart();
            }
        }
        else
        {
            Console.WriteLine("Please enter a number");
            Console.ReadKey();
            AddProductToCart();
        }
    }

    public void CheckCart()
    {
        Console.Clear();
        Console.WriteLine("The insides of your cart: \n");
        int totalValue = 0;
        foreach (Product product in Cart)
        {
            totalValue += product.Price;
            Console.WriteLine($"{product.Name}: {product.Price}$");
        }
        Console.WriteLine($"\nThe total amount will be {totalValue}$");
        Console.ReadKey();
    }
    public void CheckUserPurchaseHistory()
    {
        string[] file = File.ReadAllLines($"{name}");
    }
}
