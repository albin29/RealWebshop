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
    public Product(string data)
    {
        string[] info = data.Split(',');
        Name = info[0];
        if (int.TryParse(info[1], out int value))
        {
            Price = value;
        }
        else
        {
            throw new Exception();
        }
    }
    public override string ToString()
    {
        return ($"{Name}: {Price}$");
    }
}
