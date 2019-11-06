using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.BusinessObject
{
    public class Configuration
    {
        public string Name { get; }
        public int Value { get; }

        public Configuration(string name, int value)
        {
            Name = name;
            Value = value;
        }
    }
}
