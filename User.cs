using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop;

public class User
{
    
    public readonly string Username;
    public readonly string Password;

    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }
}
