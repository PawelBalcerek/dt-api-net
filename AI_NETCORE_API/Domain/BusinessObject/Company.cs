using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.BusinessObject
{
    public class Company
    {
        public Company(int id, string name, double indexPrice = 0)
        {
            Id = id;
            Name = name;
            IndexPrice = indexPrice;
        }

        public int Id { get; }
        public string Name { get; }
        public double IndexPrice { get; }
    }
}
