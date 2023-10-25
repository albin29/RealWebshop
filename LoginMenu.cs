namespace Webshop;

public class LoginMenu
{
    public static Boolean RegisterUser()
    {
        string[] userCSV = File.ReadAllLines("users.csv");

        string? username = string.Empty;

        while ((username.Length < 3) && (!username.Contains(" ")))
        {
            Console.Clear();
            Console.WriteLine("Please enter your desired username (at least 3 characters with no empty spaces)");
            username = Console.ReadLine();
            foreach (var item in userCSV)
            {
                string[] parts = item.Split(",");
                if (username == parts[0])
                {
                    Console.WriteLine("Sorry, username is taken");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }
            }
        }
        string? password = string.Empty;

        while (password.Length < 3)
        {
            Console.WriteLine("Please enter your password (minimum 3 characters)");
            password = Console.ReadLine();
        }

        File.AppendAllText("users.csv", $"{username},{password},{Role.Customer}\n");
        return true;
    }
    public static IUser? LoginUser()
    {
        string[] userCSV = File.ReadAllLines("users.csv");
        Console.Clear();
        Console.WriteLine("Please enter your username");

        string? username = Console.ReadLine();
        foreach (var item in userCSV)
        {
            string[] part = item.Split(",");
            string existingUsername = part[0];
            string existingPassword = part[1];

            if (username == existingUsername)
            {
                Console.WriteLine("Please enter your password");
                string? password = Console.ReadLine();

                if (password == existingPassword)
                {
                    Console.Clear();
                    if (Enum.TryParse(part[2], out Role role))
                    {
                        switch (role)
                        {
                            case Role.Customer:
                                return new Customer(username, LoadCart(username));

                            case Role.Admin:
                                return new Admin(username);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("\nWrong password.. Try again");
                    Thread.Sleep(1500);
                    Console.Clear();
                    return null;
                }
            }
            else
            {
                Console.WriteLine("\nSorry, there is no such user in our books");
                Console.ReadKey();
                Console.Clear();
                return null;
            }
        }
        return null;
    }
    private static List<Product> LoadCart(string user)
    {
        List<Product> cart = new List<Product>();
        string[] savedCart = File.ReadAllLines($"carts/{user}.csv");

        foreach (string item in savedCart)
        {
            cart.Add(new Product(item));
        }
        return cart;
    }
}
