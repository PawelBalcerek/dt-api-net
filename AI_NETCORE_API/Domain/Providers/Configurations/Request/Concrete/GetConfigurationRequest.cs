using Domain.Providers.Configurations.Request.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.Configurations.Request.Concrete
{
    public class GetConfigurationRequest : IGetConfigurationRequest
    {
        public string ConfigurationName { get; }

        public GetConfigurationRequest(string configurationName)
        {
            ConfigurationName = configurationName;
        }
    }
}
