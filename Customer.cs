namespace Webshop;
public record Customer(string name, List<Product> Cart) : IUser
{
   
    public void MainMenu()
    {
        Console.WriteLine("-------------------\nWelcome to the customer menu\n-------------------");
        Console.WriteLine("1 / Add products to cart\n2 / Check your cart\n3 / Check your buyhistory\n\n9 / Leave menu");
        string? menuChoice = Console.ReadLine();

        switch (menuChoice)
        {
            case "1":
                AddProductToCart();
                break;
            case "2":
                break;
            case "3":
                break;
            case "9":
                break;

        }
    }
    public static void AddProductToCart()
    {
        string[] products = File.ReadAllLines("../../../products.csv");

        for (int placement = 1; placement < products.Length + 1; placement++)
        {
            string[] part = products[placement].Split(",");
            string Name = part[0];
            string Price = part[1];

            Console.WriteLine("------------------------------------------\nPlease enter the number next to the product you wish to add to your cart\n------------------------------------------");
            Console.WriteLine($"{placement}. {Name} {Price}$");
            int intChoice;
            string? choice = Console.ReadLine();
            if (int.TryParse(choice, out intChoice))
            {
                Console.WriteLine($"You have {Name} for {Price} in your cart now!");
                Console.ReadKey();
                Console.Clear();
                AddProductToCart();
            }
            Console.WriteLine("Invalid input, try again please");
            Console.ReadKey();
            AddProductToCart();
        }
    }
}

