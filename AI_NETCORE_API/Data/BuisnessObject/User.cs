using System;
using System.Collections.Generic;
using System.Text;

namespace Data.BuisnessObject
{
    public class User
    {
        public User(int id, string name, string email, string password)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
        }

        public int Id { get; }
        public string Name { get; }
        public string Email { get; }
        public string Password { get; }
    }
}
