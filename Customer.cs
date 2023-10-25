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
            Console.WriteLine("1 / Add products to cart\n2 / Check your cart\n3 / Check your buyhistory\n4 / Checkout\n\n9 / Leave menu");
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
                    CheckCustomerPurchaseHistory();
                    break;
                case "4":
                    Checkout();
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
                File.AppendAllText($"carts/{name}.csv", $"{chosenProduct.Name},{chosenProduct.Price}\n");
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
    public void CheckCustomerPurchaseHistory()
    {
        Console.Clear();
        Console.WriteLine($"{name} total purchase history\n");

        string[] file = File.ReadAllLines("purchaseHistory.csv");
        int totalSpent = 0;
        foreach (string info in file)
        {
            string[] details = info.Split(',');

            string customerName = details[0], productName = details[1], productPrice = details[2], date = details[3];

            if (customerName == name)
            {
                totalSpent += int.Parse(productPrice);
                Console.WriteLine($"{productName} | {productPrice}$ | {date}");
            }
        }
        Console.WriteLine($"\nYou have spent a total of {totalSpent}$");
        Console.ReadKey();
    }
    public void Checkout()
    {
        string[] file = File.ReadAllLines($"carts/{name}.csv");

        if(Cart.Count == 0)
        {
            Console.Clear();
            Console.WriteLine("Please put something in your cart...");
            Thread.Sleep(2000);
            MainMenu();
        }

        string wholeFile = "";
        foreach (string info in file)
        {
            string[] details = info.Split(',');
            string productName = details[0];
            string productPrice = details[1];
            wholeFile += ($"{name},{productName},{productPrice},{DateTime.Now}\n");
        }

        File.AppendAllText("purchaseHistory.csv", wholeFile);
        File.WriteAllText($"carts/{name}.csv", string.Empty);
        Cart.RemoveRange(0, Cart.Count);

        string auth = "Authorizing"; 
        for (int i = 0; i < 3; i++)
        {
            Console.Clear();
            auth += '.';
            Console.WriteLine(auth);
            Thread.Sleep(700);
        }
        Console.Clear();
        Console.WriteLine("Your transaction was approved!");
        Console.ReadKey();
    }
}
