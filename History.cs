using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop;

public class History
{
    public void viewAllBuyHistory()
    {
        string[] lines = File.ReadAllLines("../../../buyHistory.csv");

        foreach (string line in lines)
        {
            lines = line.Split(',');
            string username = lines[0];
            string item = lines[1];
            string price = lines[2];
            string dateAndTime = lines[3];

            Console.WriteLine($"User: {username}, Product: {item}, Price: {price} $, Date: {dateAndTime}");

        }
    }
    public void ViewBuyHistory(string name)
    {
        string[] lines = File.ReadAllLines("../../../buyHistory.csv");

        foreach (string line in lines)
        {
            if (line == "")
            {
                continue;
            }
            lines = line.Split(',');
            string itemname = lines[0];
            string item = lines[1];
            string price = lines[2];
            string dateAndTime = lines[3];

            if (name == itemname) //why "itemname" - not username/customer?
            {
                Console.WriteLine($"Product: {item}, Price: {price} $, Date: {dateAndTime}");
            }
        }
    }

}
