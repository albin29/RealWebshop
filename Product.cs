using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop;

public record Product(
     float Price,
     string Name
);
public static class Products
{
    public static List<Product> productList = new List<Product>();
    static Products()
    {
        ReadProducts();
    }
  
    public static void RegisterProduct(Product product)
    {
        productList.Add(product);
        WriteProducts();
    }
    public static void UnregisterProduct(Product product)
    {
        productList.Remove(product);
        WriteProducts();
    }
    private static void WriteProducts()
    {
        string lines = "";
        foreach (var product in productList)
        {
            lines += product.Name + ";" + product.Price + "\n";
        }
        File.WriteAllText("../../../products.csv", lines);
    }
    public static void ReadProducts()
    {
        string[] filen = File.ReadAllLines("../../../products.csv");
        string productname = string.Empty;

        foreach (string line in filen)
        {
            if (line == "")
            {
                continue;
            }
            filen = line.Split(';');
            productname = filen[0];
            string productprice = filen[1];
            productList.Add(new Product
            (
                float.Parse(productprice),
                productname
            ));
        }
    }

    public static void ShopItems(List<Product> shoppinglist)
    {
        while (true)
        {
            Console.WriteLine("What would you like to buy? \n");

            for (int i = 0; i < productList.Count; i++)
            {
                Console.WriteLine(productList[i].Name + " kostar" + " " + productList[i].Price + " SEK ");
            }
            Console.WriteLine();
            Console.WriteLine("Press 0 to go back to menu");

            string userpick = Console.ReadLine();
            bool validitem = false;
            for (int i = 0; i < productList.Count; i++)
            {
                if (userpick == productList[i].Name)
                {
                    Console.Clear();
                    Console.WriteLine("You picked " + productList[i].Name);
                    Console.WriteLine("Press enter to proceed");
                    Console.ReadKey();
                    Console.Clear();
                    validitem = true;
                    shoppinglist.Add(productList[i]);
                    continue;
                }
            }
            if (userpick == "0")
            {
                Console.Clear();
                break;
            }
            if (!validitem)
            {
                Console.Clear();
                Console.WriteLine("Invalid item, try again ");
                Console.ReadKey();
                Console.Clear();
                continue;
            }
        }
    }
}
