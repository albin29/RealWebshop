using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop;

public class Product
{
    public readonly string Name;
    public readonly int Price;

    public Product (string name, int price)
    {
        Name = name;
        Price = price;
    }
}
