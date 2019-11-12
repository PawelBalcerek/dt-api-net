using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.BusinessObject
{
    public class User
    {
        public User(int id, string name, string email, string password, decimal cash)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            Cash = cash;
        }

        public int Id { get; }
        public string Name { get; }
        public string Email { get; }
        public string Password { get; }
        public decimal Cash { get; }  
    }
}
