using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop;

public class Product
{
    public string Name;
    public int Price;

    public Product(string name, int price)
    {
        this.Name = name;
        this.Price = price;

    }
}
