namespace Webshop;

public class LoginMenu
{
    public static void RegisterUser()
    {
        string[] userCSV = File.ReadAllLines("../../../users.csv");

        string? username = string.Empty;

        while ((username.Length < 3) && (!username.Contains(" ")))
        {
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


        File.AppendAllText("../../../users.csv", $"{username},{password},{Role.Customer}\n");
    }
    public static IUser? LoginUser()
    {
        string[] userCSV = File.ReadAllLines("../../../users.csv");

        Console.WriteLine("Please enter your username");

        foreach (var item in userCSV)
        {
            string[] part = item.Split(",");
            string existingUsername = part[0];
            string existingPassword = part[1];

            string? username = Console.ReadLine();

            if (username == existingUsername)
            {
                Console.WriteLine("Please enter your password");
                string? password = Console.ReadLine();

                if (password == existingPassword)
                {
                    if (Enum.TryParse(part[2]))
                }


            }


        }



        return null;
    }



}
