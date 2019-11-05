using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.BusinessObject
{
    public class Company
    {
        public Company(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }
        public string Name { get; }
    }
}
