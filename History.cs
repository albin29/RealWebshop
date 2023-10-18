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
        string[] filen = File.ReadAllLines("../../../buyHistory.csv");

        foreach (string line in filen)
        {
            filen = line.Split(';');
            string username = filen[0];
            string item = filen[1];
            string price = filen[2];
            string dateAndTime = filen[3];

            Console.WriteLine("User: " + username + " bought " + item + " for " + price + " sek " + " at " + dateAndTime);

        }

    }
    public void ViewBuyHistory(string name)
    {
        string[] filen = File.ReadAllLines("../../../buyHistory.csv");

        foreach (string line in filen)
        {
            if (line == "")
            {
                continue;
            }

            filen = line.Split(';');
            string itemname = filen[0];
            string item = filen[1];
            string price = filen[2];
            string dateAndTime = filen[3];

            if (name == itemname)
            {
                Console.WriteLine(item + " " + price + " " + dateAndTime);
            }
        }
    }

}