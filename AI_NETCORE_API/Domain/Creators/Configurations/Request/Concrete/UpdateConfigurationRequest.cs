using Domain.Creators.Configurations.Request.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Creators.Configurations.Request.Concrete
{
    public class UpdateConfigurationRequest : IUpdateConfigurationRequest
    {
        public string Name { get; }
        public int Value { get; }

        public UpdateConfigurationRequest(string name, int value)
        {
            Name = name;
            Value = value;
        }
    }
}
