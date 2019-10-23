using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.BusinessObject
{
    public class Company
    {
        public Company(int id, string name, int userId)
        {
            Id = id;
            Name = name;
            UserId = userId;
        }

        public int Id { get; }
        public string Name { get; }
        public int UserId { get; }
    }
}
